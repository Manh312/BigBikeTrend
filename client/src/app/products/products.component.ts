import { Component, OnInit } from '@angular/core';
import { ProductFilters, ProductResDto } from '../core/models/catalog';
import { CatalogService } from '../core/services/catalog.service';
import { Store } from '@ngrx/store';
import { BehaviorSubject, filter, Observable, tap } from 'rxjs';
import { selectProducts } from '../redux/catalog/catalog.selector';
import { loadProducts } from '../redux/catalog/catalog.action';

@Component({
  selector: 'app-products',
  standalone: false,
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent implements OnInit {
  products$: Observable<ProductResDto[]>;
  initialFilters: ProductFilters = {
    pageIndex: 1,
    pageSize: 10,
    sort: 'featured'
  };

  filters$ = new BehaviorSubject<ProductFilters>(this.initialFilters);

  maxPrice!: number;
  minPrice!: number;
  pageSize: number = 10;
  productCount!: number;

  constructor(private store: Store, private catalogService: CatalogService) { 
    this.products$ = this.store.select(selectProducts);
  }

  ngOnInit(): void {
    this.filters$.subscribe((filter) => {
      this.catalogService.getProducts(filter).subscribe((res) => {
        if (res.data?.data) {
          this.store.dispatch(loadProducts({ filters: filter }));
        }
        if (res.data?.minPrice) {
          console.log(res.data?.minPrice);
          
          this.minPrice = res.data?.minPrice;
        }
        if (res.data?.maxPrice) {
          this.maxPrice = res.data?.maxPrice;
        }
        if (res.data?.count) {
          this.productCount = res.data?.count;
        }
      });
    });
  }

  display(pageIndex: number) {
    this.initialFilters = {
      ...this.initialFilters,
      pageIndex: pageIndex
    }
    this.filters$.next(this.initialFilters);
  }

  filterChanged(filters: any) {
    this.initialFilters = {
      ...this.initialFilters,
      productCategoriesIds: filters.productCategoriesId,
      brandIds: filters.brandId,
      minPrice: filters.minPrice,
      maxPrice: filters.maxPrice,
      inStock: filters.stockType,
      ratings: filters.rating
    }
    this.filters$.next(this.initialFilters); // Update filters$ with new filters
  }

  sortFilterChanged(filters: any) {
    this.pageSize = filters.itemsToShow;
    this.initialFilters = {
      ...this.initialFilters,
      pageSize: filters.itemsToShow,
      sort: filters.sortBy
    }
    this.filters$.next(this.initialFilters);
  }
}
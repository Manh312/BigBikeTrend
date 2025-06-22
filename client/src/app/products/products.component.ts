import { Component, OnInit } from '@angular/core';
import { ProductFilters, ProductResDto } from '../core/models/catalog';
import { CatalogService } from '../core/services/catalog.service';
import { Store } from '@ngrx/store';
import { Observable, tap } from 'rxjs';
import { selectProducts } from '../redux/catalog/catalog.selector';
import { loadProducts } from '../redux/catalog/catalog.action';

@Component({
  selector: 'app-products',
  standalone: false,
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent implements OnInit {
  products$: Observable <ProductResDto[]>;
  initialFilters: ProductFilters = {
    pageIndex: 1,
    pageSize: 10,
    brandIds: [],
    productCategoriesIds: [],
    ratings: [],
    search: '',
    minPrice: null,
    inStock: null,
    maxPrice: null,
    sort: '',
    sortOrder: ''
  };

  constructor(private store: Store) { 
    this.products$ = this.store.select(selectProducts);
  }
  
  ngOnInit(): void {
  this.products$.pipe(
    tap(products => {
      if (products.length === 0)  {
        this.store.dispatch(loadProducts());
      }
    })
  ).subscribe();
}

  pageIndex!: number;
  display(pageIndex:number) {
    this.pageIndex = pageIndex;
  }

  filters!: object;
  filterChanged(filters:object) {
    console.log(filters);
    this.filters = filters;
  }

  sortFilter!: object;
  sortFilterChanged(sortFilter: object) {
    console.log(sortFilter);
    this.sortFilter = sortFilter;
  } 
}

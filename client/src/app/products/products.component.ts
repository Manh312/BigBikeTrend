import { Component, OnInit } from '@angular/core';
import { ProductFilters, ProductResDto } from '../core/models/catalog';
import { CatalogService } from '../core/services/catalog.service';

@Component({
  selector: 'app-products',
  standalone: false,
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent implements OnInit {
  products: ProductResDto[] = [];
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

  constructor(private catalogService: CatalogService) { }
  
  ngOnInit(): void {
    this.catalogService.getProducts(this.initialFilters).subscribe((res) => {
      if (res.data?.data) {
        this.products = res.data?.data;
      }
    })
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

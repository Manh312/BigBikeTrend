import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseDto } from '../models/response';
import { BrandResDto, ProductCategoriesResDto, ProductFilters, ProductPaginationRes } from '../models/catalog';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {
  constructor(private http: HttpClient) { }

  getProductCategories() {
    return this.http.get<ResponseDto<ProductCategoriesResDto[]>>('Catalog/productcategories/getall');
  }

  getBrands() {
    return this.http.get<ResponseDto<BrandResDto[]>>('Catalog/brand/getall');
  }

  getProducts(filter: ProductFilters) {
    return this.http.post<ResponseDto<ProductPaginationRes>>('Catalog/product/getall', filter);
  }
}
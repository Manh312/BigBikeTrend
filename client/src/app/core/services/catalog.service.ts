import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseDto } from '../models/response';
import { ProductCategoriesResDto } from '../models/catalog';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {
  constructor(private http: HttpClient) { }

  getProductCategories() {
    console.log('Making HTTP request...');
    return this.http.get<ResponseDto<ProductCategoriesResDto[]>>('Catalog/productcategories/getall');
  }
}
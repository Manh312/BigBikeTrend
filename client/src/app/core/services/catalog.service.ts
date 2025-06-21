import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs'; // Thêm import Observable
import { ResponseDto } from '../models/response';
import { BrandResDto, ProductCategoriesResDto, ProductFilters, ProductPaginationRes, ProductDetailsResDto } from '../models/catalog'; // Thêm ProductDetailsResDto

@Injectable({
  providedIn: 'root'
})
export class CatalogService {
  private apiUrl = 'catalog'; // Định nghĩa base URL để nhất quán

  constructor(private http: HttpClient) { }

  getProductCategories(): Observable<ResponseDto<ProductCategoriesResDto[]>> {
    return this.http.get<ResponseDto<ProductCategoriesResDto[]>>(`${this.apiUrl}/productcategories/getall`);
  }

  getBrands(): Observable<ResponseDto<BrandResDto[]>> {
    return this.http.get<ResponseDto<BrandResDto[]>>(`${this.apiUrl}/brand/getall`);
  }

  getProducts(filter: ProductFilters): Observable<ResponseDto<ProductPaginationRes>> {
    return this.http.post<ResponseDto<ProductPaginationRes>>(`${this.apiUrl}/product/getall`, filter);
  }

  getProductDetailById(id: number): Observable<ResponseDto<ProductDetailsResDto>> {
    return this.http.get<ResponseDto<ProductDetailsResDto>>(`${this.apiUrl}/productdetail/getbyid?productId=${id}`);
  }
}
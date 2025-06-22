import { createAction, props } from '@ngrx/store';
import { BrandResDto, ProductCategoriesResDto, ProductResDto } from '../../core/models/catalog';


// Product Categories
export const loadProductCategories = createAction('[Product Categories] Load Product Categories');

export const loadProductCategoriesSuccess = createAction(
  '[Catalog] Load Product Categories Success',
  props<{ productCategories: ProductCategoriesResDto[]}>()
)

export const loadProductCategoriesFailure = createAction(
  '[Catalog] Load Product Categories Failure',
  props<{ error: any}>()
)


// Brands
export const loadBrands = createAction('[Brand] Load Brands');

export const loadBrandSuccess = createAction(
  '[Brand] Load Brands Success',
  props<{ brands: BrandResDto[] }>()
)

export const loadBrandsFailure = createAction(
  '[Brand] Load Brands Failure',
  props<{ error: any}>()
)

// Product 
export const loadProducts = createAction('[Product] Load Products');

export const loadProductsSuccess = createAction(
  '[Product] Load Products Success',
  props<{ products: ProductResDto[] }>()
)

export const loadProductsFailure = createAction(
  '[Product] Load Products Failure',
  props<{ error: any}>()
)
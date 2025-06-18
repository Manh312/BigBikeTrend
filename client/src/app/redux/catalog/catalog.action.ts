import { createAction, props } from '@ngrx/store';
import { BrandResDto, ProductCategoriesResDto } from '../../core/models/catalog';

export const loadProductCategories = createAction('[Product Categories] Load Product Categories');

export const loadProductCategoriesSuccess = createAction(
  '[Catalog] Load Product Categories Success',
  props<{ productCategories: ProductCategoriesResDto[]}>()
)

export const loadProductCategoriesFailure = createAction(
  '[Catalog] Load Product Categories Failure',
  props<{ error: any}>()
)

export const loadBrands = createAction('[Brand] Load Brands');

export const loadBrandSuccess = createAction(
  '[Brand] Load Brands Success',
  props<{ brands: BrandResDto[] }>()
)

export const loadBrandsFailure = createAction(
  '[Brand] Load Brands Failure',
  props<{ error: any}>()
)
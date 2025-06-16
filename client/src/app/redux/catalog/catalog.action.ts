import { createAction, props } from '@ngrx/store';
import { ProductCategoriesResDto } from '../../core/models/catalog';

export const loadProductCategories = createAction('[Catalog] Load Product Categories');

export const loadProductCategoriesSuccess = createAction(
  '[Catalog] Load Product Categories Success',
  props<{ productCategories: ProductCategoriesResDto[]}>()
)

export const loadProductCategoriesFailure = createAction(
  '[Catalog] Load Product Categories Failure',
  props<{ error: any}>()
)
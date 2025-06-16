import { createReducer, on } from '@ngrx/store';
import { ProductCategoriesResDto } from '../../core/models/catalog';
import {
  loadProductCategories,
  loadProductCategoriesFailure,
  loadProductCategoriesSuccess,
} from './catalog.action';

export interface CatalogState {
  productCategories: ProductCategoriesResDto[];
  error: any;
}

const initialState: CatalogState = {
  productCategories: [],
  error: null,
};

export const catalogReducer = createReducer(
  initialState,
  on(loadProductCategories, (state) => ({ ...state })),
  on(loadProductCategoriesSuccess, (state, { productCategories }) => ({
    ...state,
    productCategories,
    error: null,
  })),

  on(loadProductCategoriesFailure, (state, { error }) => ({
    ...state,
    error,
  }))
);

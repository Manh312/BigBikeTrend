import { createReducer, on } from '@ngrx/store';
import {
  BrandResDto,
  ProductCategoriesResDto,
} from '../../core/models/catalog';
import {
  loadBrands,
  loadBrandsFailure,
  loadBrandSuccess,
  loadProductCategories,
  loadProductCategoriesFailure,
  loadProductCategoriesSuccess,
} from './catalog.action';

export interface CatalogState {
  productCategories: ProductCategoriesResDto[];
  brands: BrandResDto[];
  error: any;
}

const initialState: CatalogState = {
  productCategories: [],
  brands: [],
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
  })),

  on(loadBrands, (state) => ({ ...state })),
  on(loadBrandSuccess, (state, { brands }) => ({
    ...state,
    brands,
    error: null,
  })),

  on(loadBrandsFailure, (state, { error }) => ({
    ...state,
    error,
  }))
);

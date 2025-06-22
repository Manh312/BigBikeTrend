import { createReducer, on } from '@ngrx/store';
import {
  BrandResDto,
  ProductCategoriesResDto,
  ProductResDto,
} from '../../core/models/catalog';
import {
  loadBrands,
  loadBrandsFailure,
  loadBrandSuccess,
  loadProductCategories,
  loadProductCategoriesFailure,
  loadProductCategoriesSuccess,
  loadProducts,
  loadProductsFailure,
  loadProductsSuccess,
} from './catalog.action';

export interface CatalogState {
  productCategories: ProductCategoriesResDto[];
  brands: BrandResDto[];
  products: ProductResDto[];
  error: any;
}

const initialState: CatalogState = {
  productCategories: [],
  brands: [],
  products: [],
  error: null,
};

export const catalogReducer = createReducer(
  initialState,
  // Product Categories
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

  // Brands
  on(loadBrands, (state) => ({ ...state })),
  on(loadBrandSuccess, (state, { brands }) => ({
    ...state,
    brands,
    error: null,
  })),

  on(loadBrandsFailure, (state, { error }) => ({
    ...state,
    error,
  })),

  // Products
  on(loadProducts, (state) => ({ ...state })),
  on(loadProductsSuccess, (state, { products }) => ({
    ...state,
    products,
    error: null,
  })),

  on(loadProductsFailure, (state, { error }) => ({
    ...state,
    error,
  }))
);

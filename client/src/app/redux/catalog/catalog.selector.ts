import { createFeatureSelector, createSelector } from "@ngrx/store";
import { CatalogState } from "./catalog.reducer";

export const selectCatalogState = createFeatureSelector<CatalogState>('catalogStore');

export const selectProductCategories = createSelector(
  selectCatalogState,
  (state: CatalogState) => state.productCategories
)

export const selectBrands = createSelector(
  selectCatalogState,
  (state: CatalogState) => state.brands
)
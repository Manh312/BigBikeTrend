import { catalogReducer, CatalogState } from "./catalog/catalog.reducer";

export interface AppState {
  catalogStore: CatalogState
}

export const store = {
  catalogStore: catalogReducer
}
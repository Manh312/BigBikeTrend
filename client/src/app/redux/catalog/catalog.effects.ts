import { Actions, createEffect, ofType } from '@ngrx/effects';
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
import { CatalogService } from '../../core/services/catalog.service';
import { mergeMap, map, catchError, of } from 'rxjs';
import { Injectable } from '@angular/core';
import { ProductFilters } from '../../core/models/catalog';

@Injectable()
export class CatalogEffects {
  constructor(
    private action$: Actions,
    private catalogService: CatalogService
  ) { }

  // Product Categories
  loadProductCategories$ = createEffect(() =>
    this.action$.pipe(
      ofType(loadProductCategories),
      mergeMap(() =>
        this.catalogService.getProductCategories().pipe(
          map((res) => {
            return res.isSuccessed === true
              ? loadProductCategoriesSuccess({
                productCategories: res.data ? res.data : [],
              })
              : loadProductCategoriesFailure({ error: res.message });
          }),
          catchError((error) => of(loadProductCategoriesFailure({ error })))
        )
      )
    )
  );

  // Brands
  loadBrands$ = createEffect(() =>
    this.action$.pipe(
      ofType(loadBrands),
      mergeMap(() =>
        this.catalogService.getBrands().pipe(
          map((res) => {
            return res.isSuccessed
              ? loadBrandSuccess({ brands: res.data ? res.data : [] })
              : loadBrandsFailure({ error: res.message });
          }),
          catchError((error) => of(loadBrandsFailure({ error })))
        )
      )
    )
  );

  // Product
  loadProducts$ = createEffect(() =>
    this.action$.pipe(
      ofType(loadProducts),
      mergeMap(() =>
        // Provide an appropriate ProductFilters object here
        this.catalogService.getProducts({} as ProductFilters).pipe(
          map((res) => {
            return res.isSuccessed
              ? loadProductsSuccess({ products: res.data?.data ? res.data.data : [] })
              : loadProductsFailure({ error: res.message });
          }),
          catchError((error) => of(loadProductsFailure({ error })))
        )
      )
    )
  );
}

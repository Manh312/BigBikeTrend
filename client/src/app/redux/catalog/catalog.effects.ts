import { Actions, createEffect, ofType } from '@ngrx/effects';
import {
  loadProductCategories,
  loadProductCategoriesFailure,
  loadProductCategoriesSuccess,
} from './catalog.action';
import { CatalogService } from '../../core/services/catalog.service';
import { mergeMap, map, catchError, of } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class CatalogEffects {
  constructor(private action$: Actions, private catalogService: CatalogService){}

  loadProductCategories$ = createEffect(() =>
    this.action$.pipe(
      ofType(loadProductCategories),
      mergeMap(() =>
        this.catalogService.getProductCategories().pipe(
          map((res) => {
            return res.isSuccessed === true ? 
            loadProductCategoriesSuccess({productCategories: res.data ? res.data : []})
              : loadProductCategoriesFailure({ error: res.message });
          }),
          catchError((error) => of(loadProductCategoriesFailure({error})))
        )
      )
    )
  );
}

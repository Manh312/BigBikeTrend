import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { mergeMap, of } from "rxjs";
import { loadProductCategories, loadProductCategoriesSuccess } from "./catalog.action";

@Injectable()
export class CatalogEffects {
  constructor(private action$:Actions) {}

  loadProductCategories$ = createEffect(() => 
    this.action$.pipe(
      ofType(loadProductCategories),
      mergeMap(() => of(loadProductCategoriesSuccess({productCategories: []})))
    )
  )
}
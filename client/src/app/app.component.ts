import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from './redux/store';
import { Observable, tap } from 'rxjs';
import { ProductCategoriesResDto } from './core/models/catalog';
import { loadProductCategories } from './redux/catalog/catalog.action';
import { selectProductCategories } from './redux/catalog/catalog.selector';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  title = 'client';

  productCategories$: Observable<ProductCategoriesResDto[]>;
  constructor(private store: Store<AppState>){
    this.productCategories$ = this.store.select(selectProductCategories);
  }
  ngOnInit(): void {
    this.productCategories$.pipe(
      tap((productCatgories) => {
        if (productCatgories.length === 0) {
          this.store.dispatch(loadProductCategories());
          console.log('Dispatched loadProductCategories');
        }
      })
    )
    .subscribe();
  }

  
}

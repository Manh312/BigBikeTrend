import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { selectProductCategories } from '../../redux/catalog/catalog.selector';
import { footerBottomText, footerData, FooterSection } from './footer-data';
import { ProductCategoriesResDto } from '../../core/models/catalog';

@Component({
  selector: 'app-footer',
  standalone: false,
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss',
})
export class FooterComponent implements OnInit {
  footerSections: FooterSection[] = [];
  footerBottom: string = footerBottomText;
  productCategories$: Observable<ProductCategoriesResDto[]>;

  constructor(private store: Store<AppState>) {
    this.productCategories$ = this.store.select(selectProductCategories);
  }

  ngOnInit() {
    // Kết hợp footerData tĩnh với section "Loại xe" động từ productCategories$
    this.productCategories$
      .pipe(
        map((categories) => [
          {
            title: 'Loại xe',
            links: categories.map((category) => ({
              name: category.name,
              url: `/category/${category.id}`, // Thay bằng URL động dựa trên id hoặc slug
            })),
          },
          ...footerData, // Thêm các section tĩnh
        ])
      )
      .subscribe((data) => {
        this.footerSections = data;
      });
  }
}

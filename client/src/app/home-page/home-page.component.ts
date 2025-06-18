import { Component, Inject } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { CarouselModule as owlCarouselModule } from 'ngx-owl-carousel-o';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ProductCategoriesResDto } from '../core/models/catalog';
import { AppState } from '../redux/store';
import { Store } from '@ngrx/store';
import { selectProductCategories } from '../redux/catalog/catalog.selector';
import { BASE_IMAGE_API } from '../core/token/baseUrl.token';

@Component({
  selector: 'app-home-page',
  standalone: true,
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss',
  imports: [
    CommonModule,
    CarouselModule,
    owlCarouselModule,
    MatCardModule
  ]
})
export class HomePageComponent {
  myInterval: number = 2000;

  productCategories$: Observable<ProductCategoriesResDto[]>;
    constructor(private store: Store<AppState>, @Inject(BASE_IMAGE_API) public serverApi: string) {
      this.productCategories$ = this.store.select(selectProductCategories);
    }

  // Sửa đường dẫn cho slideStore
  slideStore = [
    {
      src: '/banner1.jpeg', // Đường dẫn tương đối từ assets
      alt: 'Banner 1',
    },
    {
      src: '/banner2.jpg', // Đường dẫn tương đối từ assets
      alt: 'Banner 2',
    },
    {
      src: '/banner3.jpg', // Đường dẫn tương đối từ assets
      alt: 'Banner 3',
    },
    {
      src: '/banner4.jpg', // Đường dẫn tương đối từ assets
      alt: 'Banner 4',
    },
    {
      src: '/banner5.jpg', // Đường dẫn tương đối từ assets
      alt: 'Banner 5',
    },
  ];

  customOptions: OwlOptions = {
    loop: true,
    mouseDrag: false,
    touchDrag: false,
    pullDrag: false,
    dots: false,
    navSpeed: 700,
    navText: ['', ''],
    responsive: {
      0: {
        items: 1
      },
      400: {
        items: 2
      },
      740: {
        items: 4
      },
      940: {
        items: 6
      }
    },
    nav: false,
    autoplay: true,
    autoplaySpeed: 1000,
  };

  // Sửa đường dẫn cho slidesStore
  // slidesStore: any[] = [
  //   {
  //     id: '1',
  //     src: '/iphone.png', // Đường dẫn tương đối từ assets
  //     alt: 'Iphone',
  //     title: 'Iphone'
  //   },
  //   {
  //     id: '2',
  //     src: '/ipad.png', // Đường dẫn tương đối từ assets
  //     alt: 'Ipad',
  //     title: 'Ipad'
  //   },
  //   {
  //     id: '3',
  //     src: '/airpod.png', // Đường dẫn tương đối từ assets
  //     alt: 'Airpod',
  //     title: 'Airpod'
  //   },
  //   {
  //     id: '4',
  //     src: '/macbook.png', // Đường dẫn tương đối từ assets
  //     alt: 'Macbook',
  //     title: 'Macbook'
  //   },
  //   {
  //     id: '5',
  //     src: '/applewatch.png', // Đường dẫn tương đối từ assets
  //     alt: 'Apple Watch',
  //     title: 'Apple Watch'
  //   },
  // ];
}
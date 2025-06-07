import { Component } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { CarouselModule as owlCarouselModule } from 'ngx-owl-carousel-o';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';

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
  slideStore = [
    {
      src: '/banner1.png',
      alt: 'Banner 1',
    },
    {
      src: '/banner2.jpg',
      alt: 'Banner 2',
    },
    {
      src: '/banner3.jpg',
      alt: 'Banner 3',
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
  }
  slidesStore:any[] = [
    {
      id: '1',
      src: '/iphone.png',
      alt: 'Iphone',
      title: 'Iphone'
    },
    {
      id: '2',
      src: '/ipad.png',
      alt: 'Ipad',
      title: 'Ipad'
    },
    {
      id: '3',
      src: '/airpod.png',
      alt: 'Airpod',
      title: 'Airpod'
    },
    {
      id: '4',
      src: '/macbook.png',
      alt: 'Macbook',
      title: 'Macbook'
    },
    {
      id: '5',
      src: '/applewatch.png',
      alt: 'Apple Watch',
      title: 'Apple Watch'
    },
  ]
}

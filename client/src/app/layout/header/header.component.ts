import { Observable } from 'rxjs';
import { ProductCategoriesResDto } from '../../core/models/catalog';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { Component, HostListener } from '@angular/core';
import { selectProductCategories } from '../../redux/catalog/catalog.selector';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  isMenuOpen = false;
  isMobile = window.innerWidth < 768;

  // Định nghĩa mảng danh mục với icon và đường dẫn
  productCategories = [
    { name: 'Iphone', icon: 'phone_iphone', link: '/phones' },
    { name: 'Ipad', icon: 'tablet_mac', link: '/tablets' },
    { name: 'MacBook', icon: 'laptop_mac', link: '/laptops' },
    { name: 'Tai nghe', icon: 'headphones', link: '/accessories' },
    { name: 'Đồng hồ', icon: 'watch', link: '/apple-watch' }
  ];

  navItems = [
    { name: 'Trang chủ', link: '/' },
    { name: 'Sản phẩm', link: '/products' },
    { name: 'Giới thiệu', link: '/about' },
    { name: 'Liên hệ', link: '/contact' }
  ];

  productCategories$: Observable<ProductCategoriesResDto[]>;
  constructor(private store: Store<AppState>) {
    this.productCategories$ = this.store.select(selectProductCategories);
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen; 
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.isMobile = window.innerWidth < 768;
    if (!this.isMobile) {
      this.isMenuOpen = false; // Đóng menu khi chuyển sang desktop
    }
  }
}
import { Component, Inject, Input } from '@angular/core';
import { ProductResDto } from '../../core/models/catalog';
import { BASE_IMAGE_API } from '../../core/token/baseUrl.token';

@Component({
  selector: 'app-product-card',
  standalone: false,
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.scss'
})
export class ProductCardComponent {

  constructor(@Inject(BASE_IMAGE_API) public imageUrl: string) {}
  @Input() product: ProductResDto = {
    id: 1,
    name: 'Ninja 400',
    description: 'Ninja 400 là mẫu xe đang được bán chạy nhất',
    originalPrice: 145000000,
    discountPercentage: 10,
    dicountAmount: null,
    newPrice: 135000000,
    isOnDiscount: true,
    stockQuantity: 20,
    averageRating: 0,
    totalReviews: 0,
    inStock: true,
    isFeatured: true,
    productCategoriesResDto: {
      id: 1,
      name: 'Sport Bike',
      image: null
    },
    brandResDto: {
      id: 1,
      name: 'Kawasaki',
      image: null
  },
    thumbnail: {
      imageUrl: '/kawasaki-ninja-400-abs-se-2022.jpg',
      alt: 'Kawasaki Ninja 400'
    }
  };
}

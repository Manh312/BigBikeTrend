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
  @Input() product!: ProductResDto;

  ngOnInit(): void {
    console.log('Product card initialized with:', this.product);
  }
}

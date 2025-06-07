import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { ProductsComponent } from './products.component';
import { PaginationComponent } from './pagination/pagination.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';


@NgModule({
  declarations: [
    ProductsComponent,
    PaginationComponent
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    MatButtonModule,
    MatIcon
  ]
})
export class ProductsModule { }

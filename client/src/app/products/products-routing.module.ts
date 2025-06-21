import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './products.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';


const routes: Routes = [
  {
    path: '', // Đường dẫn gốc trong module products
    component: ProductsComponent
  },
  {
    path: 'detail/:id', // Đường dẫn cho chi tiết sản phẩm, :id là tham số động
    component: ProductDetailComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule {}

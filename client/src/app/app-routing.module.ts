import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { HomePageComponent } from './home-page/home-page.component';
import { ProductsComponent } from './products/products.component';

const routes: Routes = [
  {
      path: '',
      component: HomeComponent,
      children: [
        {
          path: "",
          component: HomePageComponent
        },
        {
          path: "products",
          loadChildren: () => import('./products/products.module').then(m => m.ProductsModule)
        }
      ]
    },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

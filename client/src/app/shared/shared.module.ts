import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RatingComponent } from './components/rating/rating.component';
import { MatIcon } from '@angular/material/icon';



@NgModule({
  declarations: [
    RatingComponent
  ],
  imports: [
    CommonModule,
    MatIcon
  ],
  exports: [
    RatingComponent
  ]
})
export class SharedModule { }

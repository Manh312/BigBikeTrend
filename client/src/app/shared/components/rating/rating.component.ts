import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 's-rating',
  standalone: false,
  templateUrl: './rating.component.html',
  styleUrl: './rating.component.scss'
})
export class RatingComponent implements OnInit{
  @Input() rating: number = 0;

  ngOnInit(): void {
      this.rating = this.rating > 5 ? 5: Math.floor(this.rating);
  }
}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatSelectChange } from '@angular/material/select';

@Component({
  selector: 'app-sort-header',
  standalone: false,
  templateUrl: './sort-header.component.html',
  styleUrl: './sort-header.component.scss'
})
export class SortHeaderComponent {
  readonly showOptions: number[] = [10, 20, 30, 40, 100];
  readonly sortOptions: any[] = [
    {
      name: 'Nổi bật',
      value: 'featured'
    },
    {
      name: 'Mới nhất',
      value: 'newest'
    },
    {
      name: 'Giá: Thấp đến Cao',
      value: 'price_lth'
    },
    {
      name: 'Giá: Cao đến Thấp',
      value: 'price_htl'
    },
    {
      name: 'Đánh giá',
      value: 'rating'
    }
  ]  
  @Input() itemsToShow: number = 10;
  @Input() sortBy: string = 'Giá: Thấp đến Cao';
  @Input() pageItems!: number;

  @Output() sortHeaderChanges = new EventEmitter<any>();

  itemsToShowChange(obj: MatSelectChange) {
    this.itemsToShow = obj.value;
    this.applyChanges();
  }

  sortByChange(obj: MatSelectChange) {
    this.sortBy = obj.value;
    this.applyChanges();
  }

  applyChanges() {
    const sortFilter = {
      itemsToShow: this.itemsToShow,
      sortBy: this.sortBy
    }

    this.sortHeaderChanges.emit(sortFilter);
  }
}

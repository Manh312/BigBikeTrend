import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  standalone: false,
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.scss'
})
export class PaginationComponent {
  @Input() pageItems = 240;
  @Input() currentPage = 0;
  @Input() pageSize = 20;

  @Output() pageChange = new EventEmitter<number>();

  get totalPages():number {
    return Math.ceil(this.pageItems/this.pageSize);
  }
  
  getPaginationArray():number[] {
    var pages = [];

    if (this.currentPage <= 3) {
      pages.push(0, 1, 2, 3, -1, this.totalPages - 1);
    } 
    else if (this.currentPage >= this.totalPages - 3) {
      pages.push(0, -1, this.totalPages - 4, this.totalPages - 3, this.totalPages - 2, this.totalPages - 1);
    }
    else {
      pages.push(-1, this.currentPage - 1, this.currentPage, this.currentPage + 1, - 1);
    }

    return pages;
  };

  onPageChange(pageIndex: number) {
    if (pageIndex >= 0 && pageIndex < this.totalPages) {
      this.currentPage = pageIndex;
      this.pageChange.emit(pageIndex);
    }
  }


}

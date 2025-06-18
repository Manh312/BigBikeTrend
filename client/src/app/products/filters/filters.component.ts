import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BrandResDto, ProductCategoriesResDto } from '../../core/models/catalog';
import { Observable, tap } from 'rxjs';
import { Store } from '@ngrx/store';
import { selectBrands, selectProductCategories } from '../../redux/catalog/catalog.selector';
import { loadBrands } from '../../redux/catalog/catalog.action';

@Component({
  selector: 'app-filters',
  standalone: false,
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.scss'
})
export class FiltersComponent implements OnInit {
  productCategories$: Observable <ProductCategoriesResDto[]>

  brands$: Observable <BrandResDto[]>

  constructor(private store: Store) {
    this.productCategories$ = this.store.select(selectProductCategories);
    this.brands$ = this.store.select(selectBrands);
  }
  ngOnInit(): void {
    this.brands$.pipe(
      tap(brands => {
        if (brands.length === 0) {
          this.store.dispatch(loadBrands());
        }
      })
    )
    .subscribe();
  }

  ratings = [
    { value: 5, selected: false },
    { value: 4, selected: false },
    { value: 3, selected: false },
    { value: 2, selected: false },
    { value: 1, selected: false },
  ];

  @Input() selectedProductCategoriesIds: number[] = [];
  @Input() selectedBrandIds: number[] = [];
  @Input() selectedStockType: boolean = true;
  @Input() selectedRating: number[] = [];

  @Input() minPrice: number = 1000;
  @Input() maxPrice: number = 100000000;
  @Input() selectedMinPrice: number = this.minPrice;
  @Input() selectedMaxPrice: number = this.maxPrice;

  @Output() filtersChanged = new EventEmitter<any>();

  minPriceChange(value: number) {
    if (value <= this.selectedMaxPrice) {
      this.selectedMinPrice = value;
    } else {
      this.selectedMinPrice = this.selectedMaxPrice;
    }
    this.applyFilters();
  }

  maxPriceChange(value: number) {
    if (value >= this.selectedMinPrice) {
      this.selectedMaxPrice = value;
    } else {
      this.selectedMaxPrice = this.selectedMinPrice;
    }
    this.applyFilters();
  }

  toggleRating(ratingValue: number) {
    const index = this.selectedRating.indexOf(ratingValue);
    if (index === -1) {
      this.selectedRating.push(ratingValue);
    } else {
      this.selectedRating.splice(index, 1);
    }
    this.applyFilters();
  }

  toggleProductCategories(productCategoriesId: number) {
    const index = this.selectedProductCategoriesIds.indexOf(productCategoriesId);
    if (index === -1) {
      this.selectedProductCategoriesIds.push(productCategoriesId);
    } else {
      this.selectedProductCategoriesIds.splice(index, 1);
    }
    this.applyFilters();
  }

  toggleBrand(brandId: number) {
    const index = this.selectedBrandIds.indexOf(brandId);
    if (index === -1) {
      this.selectedBrandIds.push(brandId);
    } else {
      this.selectedBrandIds.splice(index, 1);
    }
    this.applyFilters();
  }

  toggleStock(value: boolean) {
    this.selectedStockType = value;
    this.applyFilters();
  }

  applyFilters() {
    const selectedFilters = {
      productCategoriesId: this.selectedProductCategoriesIds,
      brandId: this.selectedBrandIds,
      minPrice: this.selectedMinPrice,
      maxPrice: this.selectedMaxPrice,
      stockType: this.selectedStockType,
      rating: this.selectedRating
    };

    this.filtersChanged.emit(selectedFilters);
  }
}
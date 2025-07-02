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
  styleUrl: './filters.component.scss',
})
export class FiltersComponent implements OnInit {
  productCategories$: Observable<ProductCategoriesResDto[]>;
  brands$: Observable<BrandResDto[]>;
  priceError: string | null = null;

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
    ).subscribe();
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

  @Input() minPrice!: number;
  @Input() maxPrice!: number;
  @Input() selectedMinPrice!: number;
  @Input() selectedMaxPrice!: number;

  @Output() filtersChanged = new EventEmitter<any>();

  applyPriceFilter() {
    // Reset error
    this.priceError = null;

    // Validate price inputs
    if (this.selectedMinPrice < 0 || this.selectedMaxPrice < 0) {
      this.priceError = 'Giá không được âm';
      return;
    }
    if (this.selectedMinPrice > this.selectedMaxPrice) {
      this.priceError = 'Giá tối thiểu phải nhỏ hơn giá tối đa';
      return;
    }
    if (this.selectedMinPrice < this.minPrice) {
      this.selectedMinPrice = this.minPrice;
    }
    if (this.selectedMaxPrice > this.maxPrice) {
      this.selectedMaxPrice = this.maxPrice;
    }

    this.applyFilters();
  }

  toggleRating(ratingValue: number) {
    if (this.selectedRating.includes(ratingValue)) {
      this.selectedRating = this.selectedRating.filter(id => id !== ratingValue);
    } else {
      this.selectedRating = [...this.selectedRating, ratingValue];
    }
    this.applyFilters();
  }

  toggleProductCategories(productCategoriesId: number) {
    if (this.selectedProductCategoriesIds.includes(productCategoriesId)) {
      this.selectedProductCategoriesIds = this.selectedProductCategoriesIds.filter(id => id !== productCategoriesId);
    } else {
      this.selectedProductCategoriesIds = [...this.selectedProductCategoriesIds, productCategoriesId];
    }
    this.applyFilters();
  }

  toggleBrand(brandId: number) {
    if (this.selectedBrandIds.includes(brandId)) {
      this.selectedBrandIds = this.selectedBrandIds.filter(id => id !== brandId);
    } else {
      this.selectedBrandIds = [...this.selectedBrandIds, brandId];
    }
    this.applyFilters();
  }

  toggleStock(value: boolean) {
    this.selectedStockType = value;
    this.applyFilters();
    console.log('Toggle Stock to:', value);
  }

  applyFilters() {
    const selectedFilters = {
      productCategoriesId: this.selectedProductCategoriesIds.length > 0 ? this.selectedProductCategoriesIds : undefined,
      brandId: this.selectedBrandIds.length > 0 ? this.selectedBrandIds : undefined,
      minPrice: this.selectedMinPrice !== this.minPrice ? this.selectedMinPrice : undefined,
      maxPrice: this.selectedMaxPrice !== this.maxPrice ? this.selectedMaxPrice : undefined,
      stockType: this.selectedStockType !== true ? this.selectedStockType : undefined,
      rating: this.selectedRating.length > 0 ? this.selectedRating : undefined,
    };

    this.filtersChanged.emit(selectedFilters);
    console.log('Emitted Filters:', selectedFilters);
  }
}
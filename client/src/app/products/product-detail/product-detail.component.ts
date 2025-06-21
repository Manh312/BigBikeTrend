import { Component, OnInit } from '@angular/core';
import { ProductDetailsResDto, ProductDetailDataResDto, PowerResDto, PerformanceResDto, DetailResDto, FeatureResDto } from '../../core/models/catalog';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from '../../core/services/catalog.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  standalone: false,
  styleUrl: './product-detail.component.scss'
})
export class ProductDetailComponent implements OnInit {
  product: ProductDetailsResDto | null = null;
  parsedDetails: ProductDetailDataResDto | undefined;
  powerItems: PowerResDto[] = [];
  performanceItems: PerformanceResDto[] = [];
  detailItems: DetailResDto[] = [];
  featureItems: FeatureResDto[] = [];
  loading = true;
  error: string | null = null;

  detailCategories = [
    { name: 'CÔNG NGHỆ', open: false, items: [] as FeatureResDto[] },
    { name: 'SỨC MẠNH', open: false, items: [] as PowerResDto[] },
    { name: 'HIỆU NĂNG', open: false, items: [] as PerformanceResDto[] },
    { name: 'CHI TIẾT', open: true, items: [] as DetailResDto[] }
  ];

  constructor(
    private route: ActivatedRoute,
    private catalogService: CatalogService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadProductDetail(+id);
    } else {
      this.loading = false;
      this.error = 'Invalid product ID.';
      console.error('Product ID is missing in route.');
    }
  }

  loadProductDetail(id: number) {
    this.catalogService.getProductDetailById(id).subscribe({
      next: (res) => {
        this.loading = false;
        console.log('API Response:', res.data);
        if (res.data) {
          this.product = res.data;
          this.parsedDetails = this.product.details as ProductDetailDataResDto;
          this.powerItems = this.parsedDetails?.power || [];
          this.performanceItems = this.parsedDetails?.performance || [];
          this.detailItems = this.parsedDetails?.productSpecificDetails || [];
          this.featureItems = this.parsedDetails?.features || [];

          // Assign items to categories
          this.detailCategories.find(c => c.name === 'CÔNG NGHỆ')!.items = this.featureItems;
          this.detailCategories.find(c => c.name === 'SỨC MẠNH')!.items = this.powerItems;
          this.detailCategories.find(c => c.name === 'HIỆU NĂNG')!.items = this.performanceItems;
          this.detailCategories.find(c => c.name === 'CHI TIẾT')!.items = this.detailItems;

          if (!this.parsedDetails) {
            console.warn('parsedDetails is missing or null.');
          }
        }
      },
      error: (err) => {
        this.loading = false;
        this.error = 'Failed to load product details.';
        console.error('Error loading product detail:', err);
      }
    });
  }

  toggleCategory(category: { name: string; open: boolean; items: any[] }) {
    category.open = !category.open;
  }

  getItems(categoryName: string): { label: string; value: string; unit?: string }[] {
    const category = this.detailCategories.find(c => c.name === categoryName);
    if (!category || !category.items.length) return [];

    switch (categoryName) {
      case 'CHI TIẾT':
        return (category.items as DetailResDto[]).map(item => ({
          label: item.detailCategory,
          value: item.detailValue,
          unit: item.detailUnit ?? undefined
        }));
      case 'SỨC MẠNH':
        return (category.items as PowerResDto[]).map(item => ({
          label: item.powerCategory,
          value: item.powerValue,
          unit: item.powerUnit ?? undefined
        }));
      case 'HIỆU NĂNG':
        return (category.items as PerformanceResDto[]).map(item => ({
          label: item.performanceCategory,
          value: item.performanceValue,
          unit: item.performanceUnit ?? undefined
        }));
      case 'CÔNG NGHỆ':
        return (category.items as FeatureResDto[]).map(item => ({
          label: item.featureName,
          value: item.description || ''
        }));
      default:
        return [];
    }
  }
}
import { ImageDtoRes } from './image';
import { Pagination } from './pagination';
export interface ProductCategoriesResDto {
  id: number;
  name: string;
  image: ImageDtoRes | null;
}

export interface BrandResDto {
  id: number;
  name: string;
  image: ImageDtoRes | null;
}

export interface ProductResDto {
    id: number;
    name: string;
    description: string;
    originalPrice: number;
    discountPercentage: number | null;
    dicountAmount: number | null;
    newPrice: number;
    isOnDiscount: boolean;
    stockQuantity: number;
    averageRating: number;
    totalReviews: number;
    inStock: boolean;
    isFeatured: boolean;
    productCategoriesResDto: ProductCategoriesResDto;
    brandResDto: BrandResDto;
    thumbnail: ImageDtoRes | null;
}

export interface ProductFilters {
    pageIndex: number;
    pageSize: number;
    brandIds?: number[];
    productCategoriesIds?: number[];
    ratings?: number[];
    search?: string;
    inStock?: boolean;
    minPrice?: number;
    maxPrice?: number;
    sort?: string;
    sortOrder?: string;
}

export interface ProductPaginationRes extends Pagination<ProductResDto> {
  minPrice?: number;
  maxPrice?: number;
}

// Interface ánh xạ với lớp Power của C#
export interface PowerResDto {
  powerCategory: string; 
  powerValue: string;   
  powerUnit: string | null; 
  powerDetails: string | null; 
}

// Interface ánh xạ với lớp Performance của C#
export interface PerformanceResDto {
  performanceCategory: string; 
  performanceValue: string;    
  performanceUnit: string | null; 
  performanceDetails: string | null; 
}

// Interface ánh xạ với lớp Detail của C#
export interface DetailResDto {
  detailCategory: string;
  detailValue: string;    
  detailUnit: string | null; 
}

// Interface ánh xạ với lớp Feature của C#
export interface FeatureResDto {
  featureName: string; 
  description: string | null; 
}

export interface ProductDetailDataResDto {
  power: PowerResDto[]; 
  performance: PerformanceResDto[]; 
  productSpecificDetails: DetailResDto[]; 
  features: FeatureResDto[]; 
}


export interface ProductDetailsResDto {
  id: number;
  name: string;
  description: string;
  originalPrice: number;
  discountPercentage: number | null;
  discountAmount: number | null;
  newPrice: number;
  isOnDiscount: boolean;
  stockQuantity: number;
  averageRating: number;
  totalReviews: number;
  inStock: boolean;
  isFeatured: boolean;
  productCategory: ProductCategoriesResDto;
  brand: BrandResDto;
  thumbnail: ImageDtoRes;
  details: ProductDetailDataResDto;
}



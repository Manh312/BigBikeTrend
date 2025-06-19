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
    brandIds: number[] | null;
    productCategoriesIds: number[] | null;
    ratings: number[] | null;
    search: string | null;
    inStock: boolean | null;
    minPrice: number | null;
    maxPrice: number | null;
    sort: string | null;
    sortOrder: string | null;
}

export interface ProductPaginationRes extends Pagination<ProductResDto> {
  minPrice?: number;
  maxPrice?: number;
}
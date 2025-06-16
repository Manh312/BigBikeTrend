import { ImageDtoRes } from './image';
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
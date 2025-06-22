using Newtonsoft.Json;
using server.Entities;
using System.ComponentModel.DataAnnotations;

namespace server.Dto
{
    public class CreateProductReq
    {
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        public required string Name { get; set; }

        public required string Description { get; set; }

        [Required(ErrorMessage = "Giá gốc là bắt buộc.")]
        public decimal OriginalPrice { get; set; }

        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }

        [Required(ErrorMessage = "Số lượng tồn kho là bắt buộc.")]
        public int StockQuantity { get; set; }

        public bool IsFeatured { get; set; } = false;

        [Required(ErrorMessage = "ID thương hiệu là bắt buộc.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "ID danh mục sản phẩm là bắt buộc.")]
        public int ProductCategoryId { get; set; }

        [Required(ErrorMessage = "Hình ảnh đại diện là bắt buộc.")]
        public required IFormFile Thumbnail { get; set; }

        [Required(ErrorMessage = "Chi tiết sản phẩm là bắt buộc.")]
        public required IFormFile Details { get; set; }
    }

    public class CreateBrandReq
    {
        [Required(ErrorMessage = "Tên thương hiệu là bắt buộc.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Hình ảnh thương hiệu là bắt buộc.")]
        public required IFormFile Image { get; set; }
    }

    public class CreateProductCategoriesReq
    {
        [Required(ErrorMessage = "Tên danh mục sản phẩm là bắt buộc.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Hình ảnh danh mục sản phẩm là bắt buộc.")]
        public required IFormFile Image { get; set; }
    }

    public class CatalogSpec
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int[]? BrandIds { get; set; }
        public int[]? productCategoriesIds { get; set; }
        public int[]? Ratings { get; set; }
        public string? Search { get; set; }
        public bool? InStock { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Sort { get; set; }
        public string? SortOrder { get; set; } = "asc";
    }

    public class ProductCategoriesResDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ImageDtoRes? Image { get; set; }
    }

    public class BrandResDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ImageDtoRes? Image { get; set; }
    }

    public class ProductResDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal NewPrice { get; set; }   
        public bool IsOnDiscount { get; set; }
        public int StockQuantity { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public bool InStock { get; set; }
        public bool IsFeatured { get; set; } = false;
        public required ProductCategoriesResDto ProductCategoriesResDto { get; set; }
        public required BrandResDto BrandResDto { get; set; }
        public ImageDtoRes? Thumbnail { get; set; }
    }

    public class ProductPagination : Pagination<Entities.Product>
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    public class ProductPaginationRes : Pagination<ProductResDto>
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    public class ProductDetailResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal NewPrice { get; set; }
        public bool IsOnDiscount { get; set; }
        public int StockQuantity { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public bool InStock { get; set; }
        public bool IsFeatured { get; set; }
        public required ProductCategoriesResDto ProductCategory { get; set; }
        public required BrandResDto Brand { get; set; }
        public required ImageDtoRes Thumbnail { get; set; }
        [JsonProperty("details")]
        public required ProductDetailData Details { get; set; }
    }
}
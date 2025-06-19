using System.ComponentModel.DataAnnotations;

namespace server.Dto
{
    public class CreateProductReq
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OriginalPrice { get; set; } 
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; } 
        public int StockQuantity { get; set; }
        public bool IsFeatured { get; set; } = false;
        public int BrandId { get; set; }
        public int ProductCategoryId { get; set; }
        public IFormFile Thumbnail { get; set; }
        [Required(ErrorMessage = "Details is required")]
        public string Details { get; set; }
    }

    public class CreateBrandReq
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }

    public class CreateProductCategoriesReq
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
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
        public string Name { get; set; }
        public ImageDtoRes? Image { get; set; }
    }

    public class BrandResDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ImageDtoRes? Image { get; set; }
    }

    public class  ProductResDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DicountAmount { get; set; }
        public decimal NewPrice { get; set; }
        public bool IsOnDiscount { get; set; }
        public int StockQuantity { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public bool InStock { get; set; }
        public bool IsFeatured { get; set; } = false;
        public ProductCategoriesResDto ProductCategoriesResDto { get; set; }
        public BrandResDto BrandResDto { get; set; }
        public ImageDtoRes? Thumbnail { get; set; }
    }

    public class ProductPagination : Pagination<Entities.Product>
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    public class ProductPaginationRes: Pagination<ProductResDto>
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}

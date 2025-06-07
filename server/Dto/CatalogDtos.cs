namespace server.Dto
{
    public class CreateProductReq
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsFeatured { get; set; } = false;
        public int BrandId { get; set; }
        public int ProductCategoryId { get; set; }
        public IFormFile Thumbnail { get; set; }
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
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public int? brandId { get; set; }
        public int? productCategoriesId { get; set; }
        public string? search { get; set; }
        public bool? inStock { get; set; }
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
        public string? sort { get; set; }
        public string? sortOrder { get; set; } = "asc"; 
    }
}

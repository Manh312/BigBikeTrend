namespace server.Entities
{
    public class Product : AuditBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public bool InStock
        {
            get { return StockQuantity > 0 ? true : false; }
        }
        public bool IsFeatured { get; set; } = false;
        public int ProductCategoriesId { get; set; }
        public ProductCategories ProductCategories { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }
        public int? ThumbnailId { get; set; }
        public Image? Thumbnail { get; set; }
    }
}

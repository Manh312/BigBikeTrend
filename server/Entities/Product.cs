namespace server.Entities
{
    public class Product : AuditBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal NewPrice
        {
            get {
                if (DiscountPercentage.HasValue && DiscountPercentage.Value > 0)
                {
                    return OriginalPrice - (OriginalPrice * DiscountPercentage.Value / 100);
                }

                if (DiscountAmount.HasValue && DiscountAmount.Value > 0)
                {
                    return OriginalPrice - DiscountAmount.Value;
                }

                return OriginalPrice;
            }
        }

        public bool IsOnDiscount 
        { 
            get
            {
                return DiscountPercentage.HasValue && DiscountPercentage.Value > 0 ||
                    DiscountAmount.HasValue && DiscountAmount.Value > 0;
            }
        }


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
        // Thêm navigation property cho ProductDetails
        public ProductDetails ProductDetails { get; set; } // Quan hệ một-một hoặc một-nhiều (tùy cơ sở dữ liệu)
    }
}

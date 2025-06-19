namespace server.Entities
{
    public class ProductDetails : AuditBaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Details { get; set; }
    }

    public class ProductDetailData
    {
        public List<Power> Power { get; set; }
        public List<Performance> Performance { get; set; }
        public List<Detail> Details { get; set; }
        public List<Feature> Features { get; set; }
    }

    public class Power
    {
        public string PowerCategory { get; set; }
        public string PowerValue { get; set; }
        public string PowerUnit { get; set; }
        public string PowerDetails { get; set; } 
    }

    public class Performance
    {
        public string PerformanceCategory { get; set; }
        public string PerformanceValue { get; set; }
        public string PerformanceUnit { get; set; }
        public string PeformanceDetails { get; set; }
    }

    public class Detail
    {
        public string DetailCategory { get; set; }
        public string DetailValue { get; set; }
        public string DetailUnit { get; set; }
    }

    public class Feature
    {
        public string FeatureName { get; set; }
        public string Description { get; set; }
    }
}

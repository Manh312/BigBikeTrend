using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Entities
{
    public class ProductDetails : AuditBaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Details { get; set; }
        [NotMapped]
        [JsonIgnore]
        public ProductDetailData ParsedDetails { get; set; }
    }

    public class ProductDetailData
    {
        [JsonProperty("power")]
        public List<Power> Power { get; set; } = new List<Power>();
        [JsonProperty("performance")]

        public List<Performance> Performance { get; set; } = new List<Performance>();

        [JsonProperty("details")]
        public List<Detail> ProductSpecificDetails { get; set; }
        [JsonProperty("features")]
        public List<Feature> Features { get; set; } = new List<Feature>();
    }

    [NotMapped]
    public class Power
    {
        [JsonProperty("power_category")]
        public string PowerCategory { get; set; }

        [JsonProperty("power_value")]
        public string PowerValue { get; set; }

        [JsonProperty("power_unit")]
        public string PowerUnit { get; set; }

        [JsonProperty("power_details")]
        public string PowerDetails { get; set; }
    }

    [NotMapped]
    public class Performance
    {
        [JsonProperty("performance_category")]
        public string PerformanceCategory { get; set; }

        [JsonProperty("performance_value")]
        public string PerformanceValue { get; set; }

        [JsonProperty("performance_unit")]
        public string PerformanceUnit { get; set; }

        [JsonProperty("performance_details")]
        public string PerformanceDetails { get; set; }
    }

    [NotMapped]
    public class Detail
    {
        [JsonProperty("detail_category")]
        public string DetailCategory { get; set; }

        [JsonProperty("detail_value")]
        public string DetailValue { get; set; }

        [JsonProperty("detail_unit")]
        public string DetailUnit { get; set; }
    }

    [NotMapped]
    public class Feature
    {
        [JsonProperty("feature_name")]
        public string FeatureName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}

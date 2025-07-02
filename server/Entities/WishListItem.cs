using System.Text.Json.Serialization;

namespace server.Entities
{
    public class WishListItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WishlistId { get; set; }
        [JsonIgnore]
        public WishList Wishlist { get; set; }
    }
}

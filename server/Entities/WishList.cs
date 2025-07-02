namespace server.Entities
{
    public class WishList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = new User();
        public ICollection<WishListItem> WishListItems { get; set; }
    }
}

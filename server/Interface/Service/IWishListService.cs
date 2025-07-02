using server.Dto;
using server.Entities;

namespace server.Interface.Service
{
    public interface IWishListService
    {
        Task<WishList?> GetWishlistIncludeProductAsync(int userId);
        Task AddToWishlistAsync(AddWishlistItemDto item);
        Task RemoveFromWishlistAsync(int userId, int productId);
    }
}

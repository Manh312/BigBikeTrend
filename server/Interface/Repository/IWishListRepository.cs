using server.Entities;

namespace server.Interface.Repository
{
    public interface IWishListRepository: IGenericRepository<WishList>
    {
        Task<WishList?> GetWishlistByUserIdIncludeProductAsync(int userId);
        Task<WishList?> GetWithlistByUserIdAsync(int userId);
    }
}

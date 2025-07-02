using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Entities;
using server.Interface.Repository;

namespace server.Repository
{
    public class WishListRepository : GenericRepository<WishList>, IWishListRepository
    {
        private readonly DataContext _dataContext;
        public WishListRepository(DataContext dataContext) : base(dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<WishList?> GetWishlistByUserIdIncludeProductAsync(int userId)
        {
            return await _dataContext.WishLists
                 .Include(w => w.WishListItems)
                 .ThenInclude(wi => wi.Product)
                 .FirstOrDefaultAsync(w => w.UserId == userId);
        } 

        public async Task<WishList?> GetWithlistByUserIdAsync(int userId)
        {
            return await _dataContext.WishLists
                .Include(w => w.WishListItems)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }
    }
}

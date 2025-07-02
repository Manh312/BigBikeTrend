using server.Data;
using server.Entities;
using server.Interface.Repository;

namespace server.Repository
{
    public class WishListItemRepository : GenericRepository<WishListItem>, IWishListItemRepository
    {
        public WishListItemRepository(DataContext dataContext) : base(dataContext) 
        { 
        }
    }
}

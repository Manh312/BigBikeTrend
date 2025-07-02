using server.Dto;
using server.Entities;
using server.Interface.Repository;
using server.Interface.Service;

namespace server.Service
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository _wishListRepository;
        private readonly IWishListItemRepository _wishListItemRepository;
        private readonly IProductRepository _productRepository;

        public WishListService(IWishListRepository wishListRepository, IWishListItemRepository wishListItemRepository, IProductRepository productRepository)
        {
            this._wishListRepository = wishListRepository;
            this._wishListItemRepository = wishListItemRepository;
            this._productRepository = productRepository;
        }

        public async Task AddToWishlistAsync(AddWishlistItemDto item)
        {
            var Wishlist = await _wishListRepository.GetWithlistByUserIdAsync(item.UserId);
            if (Wishlist == null)
            {
                Wishlist = new WishList()
                {
                    UserId = item.UserId,
                    WishListItems = new List<WishListItem>()
                };
                await _wishListRepository.AddAsync(Wishlist);
            }

            var productExists = await _productRepository.GetByIdAsync(item.ProductId);
            if (productExists == null)
            {
                throw new Exception("Sản phẩm không tìm thấy");
            }

            var itemExists = Wishlist.WishListItems.Any(x => x.ProductId == item.ProductId);
            if (!itemExists)
            {
                await _wishListItemRepository.AddAsync(
                    new WishListItem()
                    {
                        ProductId = item.ProductId,
                        WishlistId = Wishlist.Id
                    }
                 );
            }

        }

        public async Task<WishList?> GetWishlistIncludeProductAsync(int userId)
        {
            return await _wishListRepository.GetWishlistByUserIdIncludeProductAsync(userId);
        }

        public async Task RemoveFromWishlistAsync(int userId, int productId)
        {
            var wishlist = await _wishListRepository.GetWishlistByUserIdIncludeProductAsync(userId);
            if (wishlist == null)
            {
                throw new Exception("Danh sách mong muốn không tìm thấy");
            }

            var wishlistItem = wishlist.WishListItems.FirstOrDefault(wi => wi.ProductId == productId);
            if (wishlistItem == null)
            {
                throw new Exception("Sản phẩm không tìm thấy trong danh sách mong muốn");
            }
            await _wishListItemRepository.DeleteAsync(wishlistItem);
        }
    }
}

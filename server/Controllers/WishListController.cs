using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Interface.Repository;
using server.Interface.Service;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService wishListService;
        private readonly IMapper mapper;
        public WishListController(IWishListService wishListService, IMapper mapper)
        {
            this.wishListService = wishListService;
            this.mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<WishListItemResDto>>> GetWishList(int userId)
        {
            var wishlist = await wishListService.GetWishlistIncludeProductAsync(userId);
            if (wishlist is null)
            {
                return NotFound();
            }
            var wishlistRes = mapper.Map<IEnumerable<WishListItemResDto>>(wishlist.WishListItems);
            return Ok(wishlistRes);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ResponseDto>> AddToWishList([FromBody] AddWishlistItemDto wishlistItemDto)
        {
            ResponseDto responseDto = new ResponseDto();
            await wishListService.AddToWishlistAsync(wishlistItemDto);
            return Ok(responseDto);
        }

        [HttpDelete("Remove/{userId}/{productId}")]
        public async Task<ActionResult<ResponseDto>> RemoveFromWishList (int userId, int productId)
        {
            ResponseDto responseDto = new ResponseDto();
            await wishListService.RemoveFromWishlistAsync (userId, productId);
            return Ok(responseDto.IsSuccessed);
        }
    }
}

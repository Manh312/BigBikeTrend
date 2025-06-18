using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Entities;
using server.Interface.Service;
using Product = server.Entities.Product;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;

        public CatalogController(ICatalogService catalogService, IMapper mapper)
        {
            this._catalogService = catalogService;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("product/getall")]
        public async Task<ActionResult<ResponseDto>> GetAllProducts(CatalogSpec catalogSpec)
        {
            ResponseDto responseDto = new ResponseDto();
            ProductPagination res = await _catalogService.GetAllProducts(catalogSpec);

            responseDto.Data = new ProductPaginationRes()
            {
                PageIndex = res.PageIndex,
                PageSize = res.PageSize,
                Data = _mapper.Map<IReadOnlyList<ProductResDto>>(res.Data),
                Count = res.Count,
                MinPrice = res.MinPrice,
                MaxPrice = res.MaxPrice,
            };
            return Ok(responseDto);
        }

        [HttpPost()]
        [Route("product/create")]
        public async Task<ActionResult<ResponseDto>> CreateProduct(CreateProductReq newProduct)
        {
            Product product = await _catalogService.CreateProduct(newProduct);
            ResponseDto responseDto = new ResponseDto();
            responseDto.Data = product;
            return Ok(responseDto);
        }

        [HttpDelete()]
        [Route("product/delete/{productId}")]
        public async Task<ActionResult<ResponseDto>> DeleteProducts(int productId)
        {
            ResponseDto responseDto = new ResponseDto();

            await _catalogService.DeleteProduct(productId);
            return Ok(responseDto);
        }

        // Product Categories API Endpoints
        [HttpGet]
        [Route("productcategories/getall")]
        public async Task<ActionResult<ResponseDto>> GetAllProductCategories()
        {
            ResponseDto responseDto = new ResponseDto();
            IEnumerable<ProductCategories> productCategories = await _catalogService.GetAllProductCategories();

            responseDto.Data = _mapper.Map<IEnumerable<ProductCategoriesResDto>>(productCategories);
            return Ok(responseDto);
        }

        [HttpPost()]
        [Route("productcategories/create")]
        public async Task<ActionResult<ResponseDto>> CreateProductCategories(CreateProductCategoriesReq newProductCategories)
        {
            ProductCategories productCategories = await _catalogService.CreateProductCategories(newProductCategories);
            ResponseDto responseDto = new ResponseDto();
            responseDto.Data = productCategories;
            return Ok(responseDto);
        }

        [HttpDelete()]
        [Route("productcategories/delete/{productId}")]
        public async Task<ActionResult<ResponseDto>> DeleteProductCategories(int productCategoriesId)
        {
            ResponseDto responseDto = new ResponseDto();

            await _catalogService.DeleteProductCategories(productCategoriesId);
            return Ok(responseDto);
        }

        // Brand API Endpoints
        [HttpGet]
        [Route("brand/getall")]
        public async Task<ActionResult<ResponseDto>> GetAllBrand()
        {
            ResponseDto responseDto = new ResponseDto();
            IEnumerable<Brand> brands = await _catalogService.GetAllBrand();

            responseDto.Data = _mapper.Map<IEnumerable<BrandResDto>>(brands);
            return Ok(responseDto);
        }

        [HttpPost()]
        [Route("brand/create")]
        public async Task<ActionResult<ResponseDto>> CreateBrand(CreateBrandReq newBrand)
        {
            Brand brand = await _catalogService.CreateBrand(newBrand);
            ResponseDto responseDto = new ResponseDto();
            responseDto.Data = brand;
            return Ok(responseDto);
        }

        [HttpDelete()]
        [Route("brand/delete/{productId}")]
        public async Task<ActionResult<ResponseDto>> DeleteBrand(int brandId)
        {
            ResponseDto responseDto = new ResponseDto();

            await _catalogService.DeleteBrand(brandId);
            return Ok(responseDto);
        }
    }
}

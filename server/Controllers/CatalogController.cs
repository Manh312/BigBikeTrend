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
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("product/getall")]
        public async Task<ActionResult<ResponseDto>> GetAllProducts(CatalogSpec catalogSpec)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                if (catalogSpec == null)
                {
                    responseDto.Message = "Thông số danh mục không được để trống.";
                    return BadRequest(responseDto);
                }

                if (catalogSpec.PageIndex < 0 || catalogSpec.PageSize <= 0)
                {
                    responseDto.Message = "Tham số phân trang không hợp lệ. PageIndex phải không âm và PageSize phải lớn hơn 0.";
                    return BadRequest(responseDto);
                }

                ProductPagination res = await _catalogService.GetAllProducts(catalogSpec);

                var productResDtos = _mapper.Map<IReadOnlyList<ProductResDto>>(res.Data);

                responseDto.Data = new ProductPaginationRes()
                {
                    PageIndex = res.PageIndex,
                    PageSize = res.PageSize,
                    Data = productResDtos,
                    Count = res.Count,
                    MinPrice = res.MinPrice,
                    MaxPrice = res.MaxPrice,
                };
                if (!productResDtos.Any())
                {
                    responseDto.Message = "Không có dữ liệu sản phẩm.";
                }
                else
                {
                    responseDto.Message = "Lấy danh sách sản phẩm thành công.";
                }
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = $"Lỗi khi lấy danh sách sản phẩm: {ex.Message}";
                return StatusCode(500, responseDto);
            }
        }

        [HttpPost]
        [Route("product/create")]
        public async Task<ActionResult<ResponseDto>> CreateProduct(CreateProductReq newProduct)
        {
            ResponseDto responseDto = new ResponseDto();
            if (!ModelState.IsValid)
            {
                responseDto.Message = "Dữ liệu đầu vào không hợp lệ. Vui lòng kiểm tra lại các trường yêu cầu.";
                return BadRequest(responseDto);
            }
            try
            {
                if (newProduct == null)
                {
                    responseDto.Message = "Dữ liệu sản phẩm không được để trống.";
                    return BadRequest(responseDto);
                }

                if (string.IsNullOrEmpty(newProduct.Name) || newProduct.OriginalPrice <= 0 || newProduct.ProductCategoryId <= 0 || newProduct.BrandId <= 0)
                {
                    responseDto.Message = "Dữ liệu sản phẩm không hợp lệ. Tên, giá, ID danh mục sản phẩm và ID thương hiệu là bắt buộc.";
                    return BadRequest(responseDto);
                }

                Product product = await _catalogService.CreateProduct(newProduct);
                responseDto.Data = _mapper.Map<ProductResDto>(product);
                responseDto.Message = "Tạo sản phẩm thành công.";
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = $"Lỗi khi tạo sản phẩm: {ex.Message}";
                return BadRequest(responseDto);
            }
        }

        [HttpDelete]
        [Route("product/delete/{productId}")]
        public async Task<ActionResult<ResponseDto>> DeleteProducts(int productId)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                if (productId <= 0)
                {
                    responseDto.Message = "ID sản phẩm không hợp lệ.";
                    return BadRequest(responseDto);
                }

                await _catalogService.DeleteProduct(productId);
                responseDto.Message = "Xóa sản phẩm thành công.";
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = $"Lỗi khi xóa sản phẩm: {ex.Message}";
                return NotFound(responseDto);
            }
        }

        [HttpGet]
        [Route("productcategories/getall")]
        public async Task<ActionResult<ResponseDto>> GetAllProductCategories()
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                IEnumerable<ProductCategories> productCategories = await _catalogService.GetAllProductCategories();

                var productCategoriesResDtos = _mapper.Map<IEnumerable<ProductCategoriesResDto>>(productCategories);
                responseDto.Data = productCategoriesResDtos;
                if (!productCategoriesResDtos.Any())
                {
                    responseDto.Message = "Không có dữ liệu danh mục sản phẩm.";
                }
                else
                {
                    responseDto.Message = "Lấy danh sách danh mục sản phẩm thành công.";
                }
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = $"Lỗi khi lấy danh sách danh mục sản phẩm: {ex.Message}";
                return StatusCode(500, responseDto);
            }
        }

        [HttpPost]
        [Route("productcategories/create")]
        public async Task<ActionResult<ResponseDto>> CreateProductCategories(CreateProductCategoriesReq newProductCategories)
        {
            ResponseDto responseDto = new ResponseDto();
            if (!ModelState.IsValid)
            {
                responseDto.Message = "Dữ liệu đầu vào không hợp lệ. Vui lòng kiểm tra lại các trường yêu cầu.";
                return BadRequest(responseDto);
            }
            try
            {
                if (newProductCategories == null || string.IsNullOrEmpty(newProductCategories.Name))
                {
                    responseDto.Message = "Dữ liệu danh mục sản phẩm không hợp lệ. Tên là bắt buộc.";
                    return BadRequest(responseDto);
                }

                ProductCategories productCategories = await _catalogService.CreateProductCategories(newProductCategories);
                responseDto.Data = _mapper.Map<ProductCategoriesResDto>(productCategories);
                responseDto.Message = "Tạo danh mục sản phẩm thành công.";
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = $"Lỗi khi tạo danh mục sản phẩm: {ex.Message}";
                return BadRequest(responseDto);
            }
        }

        [HttpDelete]
        [Route("productcategories/delete/{productCategoriesId}")]
        public async Task<ActionResult<ResponseDto>> DeleteProductCategories(int productCategoriesId)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                if (productCategoriesId <= 0)
                {
                    responseDto.Message = "ID danh mục sản phẩm không hợp lệ.";
                    return BadRequest(responseDto);
                }

                await _catalogService.DeleteProductCategories(productCategoriesId);
                responseDto.Message = "Xóa danh mục sản phẩm thành công.";
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = $"Lỗi khi xóa danh mục sản phẩm: {ex.Message}";
                return NotFound(responseDto);
            }
        }

        [HttpGet]
        [Route("brand/getall")]
        public async Task<ActionResult<ResponseDto>> GetAllBrand()
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                IEnumerable<Brand> brands = await _catalogService.GetAllBrand();

                var brandResDtos = _mapper.Map<IEnumerable<BrandResDto>>(brands);
                responseDto.Data = brandResDtos;
                if (!brandResDtos.Any())
                {
                    responseDto.Message = "Không có dữ liệu thương hiệu.";
                }
                else
                {
                    responseDto.Message = "Lấy danh sách thương hiệu thành công.";
                }
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = $"Lỗi khi lấy danh sách thương hiệu: {ex.Message}";
                return StatusCode(500, responseDto);
            }
        }

        [HttpPost]
        [Route("brand/create")]
        public async Task<ActionResult<ResponseDto>> CreateBrand(CreateBrandReq newBrand)
        {
            ResponseDto responseDto = new ResponseDto();
            if (!ModelState.IsValid)
            {
                responseDto.Message = "Dữ liệu đầu vào không hợp lệ. Vui lòng kiểm tra lại các trường yêu cầu.";
                return BadRequest(responseDto);
            }
            try
            {
                if (newBrand == null || string.IsNullOrEmpty(newBrand.Name))
                {
                    responseDto.Message = "Dữ liệu thương hiệu không hợp lệ. Tên là bắt buộc.";
                    return BadRequest(responseDto);
                }

                Brand brand = await _catalogService.CreateBrand(newBrand);
                responseDto.Data = _mapper.Map<BrandResDto>(brand);
                responseDto.Message = "Tạo thương hiệu thành công.";
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = $"Lỗi khi tạo thương hiệu: {ex.Message}";
                return BadRequest(responseDto);
            }
        }

        [HttpDelete]
        [Route("brand/delete/{brandId}")]
        public async Task<ActionResult<ResponseDto>> DeleteBrand(int brandId)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                if (brandId <= 0)
                {
                    responseDto.Message = "ID thương hiệu không hợp lệ.";
                    return BadRequest(responseDto);
                }

                await _catalogService.DeleteBrand(brandId);
                responseDto.Message = "Xóa thương hiệu thành công.";
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = $"Lỗi khi xóa thương hiệu: {ex.Message}";
                return NotFound(responseDto);
            }
        }

        [HttpGet]
        [Route("productdetail/getbyid")]
        public async Task<ActionResult<ResponseDto>> GetProductDetailByProductId(int productId)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                if (productId <= 0)
                {
                    responseDto.Message = "ID sản phẩm không hợp lệ.";
                    return BadRequest(responseDto);
                }

                ProductDetails productDetails = await _catalogService.GetProductDetailsByProductId(productId);
                responseDto.Data = _mapper.Map<ProductDetailsResDto>(productDetails);
                responseDto.Message = "Lấy chi tiết sản phẩm thành công.";
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = $"Lỗi khi lấy chi tiết sản phẩm: {ex.Message}";
                return NotFound(responseDto);
            }
        }
    }
}
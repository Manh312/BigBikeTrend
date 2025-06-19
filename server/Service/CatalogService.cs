using AutoMapper;
using server.Dto;
using server.Entities;
using server.Interface.Repository;
using server.Interface.Service;
using server.Interface.Services;
using System.Text.Json;
using Product = server.Entities.Product;

namespace server.Service
{
    public class CatalogService : ICatalogService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoriesRepository _productCategoriesRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IProductDetailsRepository _productDetailsRepository; 

        public CatalogService(
            IProductRepository productRepository,
            IProductCategoriesRepository productCategoriesRepository,
            IBrandRepository brandRepository,
            IImageService imageService,
            IMapper mapper,
            IProductDetailsRepository productDetailsRepository // Thêm tham số
            )
        {
            this._productRepository = productRepository;
            this._productCategoriesRepository = productCategoriesRepository;
            this._brandRepository = brandRepository;
            this._imageService = imageService;
            this._mapper = mapper;
            this._productDetailsRepository = productDetailsRepository; // Khởi tạo
        }

        public async Task<Brand> CreateBrand(CreateBrandReq inData)
        {
            Image image = await this._imageService.SaveImageAsync(inData.Image);
            Brand brand = _mapper.Map<Brand>(inData);
            brand.ImageId = image.Id;
            return await _brandRepository.AddAsync(brand);
        }

        public async Task<ProductCategories> CreateProductCategories(CreateProductCategoriesReq inData)
        {
            Image image = await this._imageService.SaveImageAsync(inData.Image);
            ProductCategories productCategories = _mapper.Map<ProductCategories>(inData);
            productCategories.Image = image;
            return await _productCategoriesRepository.AddAsync(productCategories);
        }

        public async Task<Entities.Product> CreateProduct(CreateProductReq inData)
        {
            ProductCategories? productCategories = await this._productCategoriesRepository.GetByIdAsync(inData.ProductCategoryId);
            Brand? brand = await this._brandRepository.GetByIdAsync(inData.BrandId);

            if (productCategories == null)
            {
                throw new Exception($"Invalid Product Categories Id {inData.ProductCategoryId}");
            }
            if (brand == null)
            {
                throw new Exception($"Invalid Brand Id {inData.BrandId}");
            }

            // Xác thực JSON
            if (string.IsNullOrEmpty(inData.Details))
            {
                throw new ArgumentException("Details cannot be empty", nameof(inData.Details));
            }
            try
            {
                JsonDocument.Parse(inData.Details); // Kiểm tra JSON hợp lệ
            }
            catch (JsonException)
            {
                throw new ArgumentException("Invalid JSON format in Details", nameof(inData.Details));
            }

            Image image = await this._imageService.SaveImageAsync(inData.Thumbnail);

            Product newProduct = _mapper.Map<Product>(inData);

            newProduct.ProductCategories = productCategories;
            newProduct.Brand = brand;
            newProduct.Thumbnail = image;

            using var transaction = await _productRepository.BeginTransactionAsync();
            try
            {
                newProduct = await this._productRepository.AddAsync(newProduct);

                ProductDetails productDetails = new ProductDetails
                {
                    ProductId = newProduct.Id,
                    Details = inData.Details
                };
                await _productDetailsRepository.AddAsync(productDetails);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            return newProduct;
        }

        public async Task DeleteBrand(int brandId)
        {
            Brand? brand = await this._brandRepository.GetByIdAsync(brandId);
            if (brand == null)
            {
                throw new Exception($"Invalid Brand Id {brandId}");
            }

            await _imageService.DeleteImageAsync(brand.ImageId.Value);

            await _brandRepository.DeleteAsync(brand);
        }

        public async Task DeleteProductCategories(int productCategoriesId)
        {
            ProductCategories? productCategories = await _productCategoriesRepository.GetByIdAsync(productCategoriesId);
            if (productCategories == null)
            {
                throw new Exception($"Invalid Product Categories Id {productCategoriesId}");
            }

            await _imageService.DeleteImageAsync(productCategories.ImageId.Value);

            await _productCategoriesRepository.DeleteAsync(productCategories);
        }

        public async Task DeleteProduct(int productId)
        {
            Product? product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new Exception($"Invalid Product Id {productId}");
            }

            await _imageService.DeleteImageAsync(product.ThumbnailId.Value);
            // Product_Details sẽ tự động xóa do Cascade trong DataContext
            await _productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<Brand>> GetAllBrand()
        {
            return await _brandRepository.GetAllIncludingImage();
        }

        public async Task<IEnumerable<ProductCategories>> GetAllProductCategories()
        {
            return await _productCategoriesRepository.GetAllIncludingImage();
        }

        public async Task<ProductPagination> GetAllProducts(CatalogSpec inData)
        {
            return await _productRepository.GetAllIncludingChildEntities(inData);
        }

        public async Task<ProductDetails> GetProductDetailsByProductId(int productId)
        {
            return await _productDetailsRepository.GetByProductIdAsync(productId)
                ?? throw new Exception($"No details found for Product Id {productId}");
        }
    }
}
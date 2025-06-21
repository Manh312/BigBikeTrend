using AutoMapper;
using Newtonsoft.Json;
using server.Dto;
using server.Entities;
using server.Interface.Repository;
using server.Interface.Service;
using server.Interface.Services;
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
            IProductDetailsRepository productDetailsRepository)
        {
            _productRepository = productRepository;
            _productCategoriesRepository = productCategoriesRepository;
            _brandRepository = brandRepository;
            _imageService = imageService;
            _mapper = mapper;
            _productDetailsRepository = productDetailsRepository;
        }

        public async Task<Brand> CreateBrand(CreateBrandReq inData)
        {
            if (inData == null)
                throw new ArgumentNullException(nameof(inData), "Dữ liệu thương hiệu không được để trống.");

            Image image = await _imageService.SaveImageAsync(inData.Image);
            Brand brand = _mapper.Map<Brand>(inData);
            brand.ImageId = image.Id;
            return await _brandRepository.AddAsync(brand);
        }

        public async Task<ProductCategories> CreateProductCategories(CreateProductCategoriesReq inData)
        {
            if (inData == null)
                throw new ArgumentNullException(nameof(inData), "Dữ liệu danh mục sản phẩm không được để trống.");

            Image image = await _imageService.SaveImageAsync(inData.Image);
            ProductCategories productCategories = _mapper.Map<ProductCategories>(inData);
            productCategories.Image = image;
            return await _productCategoriesRepository.AddAsync(productCategories);
        }

        public async Task<Product> CreateProduct(CreateProductReq inData)
        {
            if (inData == null)
                throw new ArgumentNullException(nameof(inData), "Dữ liệu sản phẩm không được để trống.");

            ProductCategories? productCategories = await _productCategoriesRepository.GetByIdAsync(inData.ProductCategoryId);
            Brand? brand = await _brandRepository.GetByIdAsync(inData.BrandId);

            if (productCategories == null)
                throw new Exception($"ID danh mục sản phẩm {inData.ProductCategoryId} không hợp lệ.");
            if (brand == null)
                throw new Exception($"ID thương hiệu {inData.BrandId} không hợp lệ.");

            if (inData.Details == null || inData.Details.Length == 0)
                throw new ArgumentException("Chi tiết sản phẩm không được để trống.", nameof(inData.Details));

            // Đọc nội dung từ file Details
            using var reader = new StreamReader(inData.Details.OpenReadStream());
            string detailsJson = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(detailsJson))
                throw new ArgumentException("Chi tiết sản phẩm không được để trống.", nameof(inData.Details));

            try
            {
                System.Text.Json.JsonDocument.Parse(detailsJson);
            }
            catch (JsonException)
            {
                throw new ArgumentException("Định dạng JSON trong chi tiết sản phẩm không hợp lệ.", nameof(inData.Details));
            }

            Image image = await _imageService.SaveImageAsync(inData.Thumbnail);

            Product newProduct = _mapper.Map<Product>(inData);
            newProduct.ProductCategories = productCategories;
            newProduct.Brand = brand;
            newProduct.Thumbnail = image;

            // Tính discountAmount
            if (inData.DiscountPercentage.HasValue) // Giả định thêm thuộc tính DiscountPercentage
            {
                newProduct.DiscountAmount = newProduct.OriginalPrice * inData.DiscountPercentage.Value / 100;
            }

            using var transaction = await _productRepository.BeginTransactionAsync();
            try
            {
                newProduct = await _productRepository.AddAsync(newProduct);

                ProductDetails productDetails = new ProductDetails
                {
                    ProductId = newProduct.Id,
                    Details = detailsJson
                };
                await _productDetailsRepository.AddAsync(productDetails);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new Exception("Lỗi khi tạo sản phẩm và chi tiết sản phẩm.");
            }

            return newProduct;
        }

        public async Task DeleteBrand(int brandId)
        {
            Brand? brand = await _brandRepository.GetByIdAsync(brandId);
            if (brand == null)
                throw new Exception($"ID thương hiệu {brandId} không hợp lệ.");

            await _imageService.DeleteImageAsync(brand.ImageId.Value);
            await _brandRepository.DeleteAsync(brand);
        }

        public async Task DeleteProductCategories(int productCategoriesId)
        {
            ProductCategories? productCategories = await _productCategoriesRepository.GetByIdAsync(productCategoriesId);
            if (productCategories == null)
                throw new Exception($"ID danh mục sản phẩm {productCategoriesId} không hợp lệ.");

            await _imageService.DeleteImageAsync(productCategories.ImageId.Value);
            await _productCategoriesRepository.DeleteAsync(productCategories);
        }

        public async Task DeleteProduct(int productId)
        {
            Product? product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception($"ID sản phẩm {productId} không hợp lệ.");

            await _imageService.DeleteImageAsync(product.ThumbnailId.Value);
            await _productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<Brand>> GetAllBrand()
        {
            var brands = await _brandRepository.GetAllIncludingImage();
            if (!brands.Any())
                throw new Exception("Không có dữ liệu thương hiệu.");
            return brands;
        }

        public async Task<IEnumerable<ProductCategories>> GetAllProductCategories()
        {
            var productCategories = await _productCategoriesRepository.GetAllIncludingImage();
            if (!productCategories.Any())
                throw new Exception("Không có dữ liệu danh mục sản phẩm.");
            return productCategories;
        }

        public async Task<ProductPagination> GetAllProducts(CatalogSpec inData)
        {
            if (inData == null)
                throw new ArgumentNullException(nameof(inData), "Thông số danh mục không được để trống.");

            var products = await _productRepository.GetAllIncludingChildEntities(inData);
            if (!products.Data.Any())
                throw new Exception("Không có dữ liệu sản phẩm.");
            return products;
        }

        public async Task<ProductDetails> GetProductDetailsByProductId(int productId)
        {
            var productDetails = await _productDetailsRepository.GetByProductIdAsync(productId);
            if (productDetails == null)
                throw new Exception($"Không tìm thấy chi tiết sản phẩm cho ID sản phẩm {productId}.");

            // Khởi tạo ParsedDetails với giá trị mặc định
            productDetails.ParsedDetails = new ProductDetailData
            {
                Power = new List<Power>(),
                Performance = new List<Performance>(),
                ProductSpecificDetails = new List<Detail>(),
                Features = new List<Feature>()
            };

            // Parse JSON từ Details nếu có
            if (!string.IsNullOrEmpty(productDetails.Details) && productDetails.Details != "{}")
            {
                try
                {
                    var settings = new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Populate
                    };
                    var parsedData = JsonConvert.DeserializeObject<ProductDetailData>(productDetails.Details, settings);
                    if (parsedData != null)
                    {
                        productDetails.ParsedDetails = parsedData;
                    }
                    else
                    {
                        Console.WriteLine($"Parsed data is null for ProductId {productId}.");
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON Parse error for ProductId {productId}: {ex.Message} - Details: {productDetails.Details}");
                }
            }
            else
            {
                Console.WriteLine($"No valid Details for ProductId {productId}, using default.");
            }

            return productDetails;
        }
    }
}
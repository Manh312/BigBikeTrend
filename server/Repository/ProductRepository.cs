using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using server.Data;
using server.Dto;
using server.Entities;
using server.Interface.Repository;
using Product = server.Entities.Product;

namespace server.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<ProductPagination> GetAllIncludingChildEntities(CatalogSpec inData)
        {
            if (inData == null)
                throw new ArgumentNullException(nameof(inData), "Thông số danh mục không được để trống.");

            IQueryable<Product> productQuery = _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.Brand)
                .Include(p => p.Thumbnail)
                .Include(p => p.ProductDetails)
                .AsQueryable();

            // Áp dụng các bộ lọc
            if (!string.IsNullOrEmpty(inData.Search))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(inData.Search));
            }

            if (inData.MinPrice.HasValue)
            {
                productQuery = productQuery.Where(p => p.OriginalPrice >= inData.MinPrice.Value);
            }

            if (inData.MaxPrice.HasValue)
            {
                productQuery = productQuery.Where(p => p.OriginalPrice <= inData.MaxPrice.Value);
            }

            if (inData.InStock.HasValue)
            {
                productQuery = productQuery.Where(p => inData.InStock.Value ? p.StockQuantity > 0 : p.StockQuantity <= 0);
            }

            if (inData.productCategoriesIds != null && inData.productCategoriesIds.Length > 0)
            {
                productQuery = productQuery.Where(p => inData.productCategoriesIds.Contains(p.ProductCategoriesId));
            }

            if (inData.BrandIds != null && inData.BrandIds.Length > 0)
            {
                productQuery = productQuery.Where(p => inData.BrandIds.Contains(p.BrandId));
            }

            // Áp dụng sắp xếp
            if (!string.IsNullOrEmpty(inData.Sort))
            {
                switch (inData.Sort.ToLower())
                {
                    case "price_htl":
                        productQuery = productQuery.OrderByDescending(p => p.OriginalPrice);
                        break;
                    case "price_lth":
                        productQuery = productQuery.OrderBy(p => p.OriginalPrice);
                        break;
                    case "featured":
                        productQuery = productQuery.OrderByDescending(p => p.IsFeatured);
                        break;
                    case "rating":
                        productQuery = productQuery.OrderBy(p => p.AverageRating);
                        break;
                    case "newest":
                        productQuery = productQuery.OrderByDescending(p => p.CreatedDate);
                        break;
                }
            }

            // Thực hiện phân trang và lấy dữ liệu
            var totalCount = await productQuery.CountAsync();
            var data = await productQuery
                .Skip((inData.PageIndex - 1) * inData.PageSize)
                .Take(inData.PageSize)
                .ToListAsync();

            var minPrice = data.Any() ? data.Min(p => p.OriginalPrice) : 0m;
            var maxPrice = data.Any() ? data.Max(p => p.OriginalPrice) : 0m;

            return new ProductPagination
            {
                PageIndex = inData.PageIndex,
                PageSize = inData.PageSize,
                Data = data,
                Count = totalCount,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };
        }
    }
}

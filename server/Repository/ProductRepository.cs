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
            IQueryable<Product> productQuery = _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.Brand)
                .Include(p => p.Thumbnail)
                .AsQueryable();

            // Thực hiện join với Product_Details
            productQuery = productQuery
                .GroupJoin(_context.ProductDetails,
                    p => p.Id,
                    pd => pd.ProductId,
                    (product, details) => new { Product = product, Details = details.DefaultIfEmpty() })
                .SelectMany(x => x.Details.DefaultIfEmpty(),
                    (product, detail) => new Product
                    {
                        Id = product.Product.Id,
                        Name = product.Product.Name,
                        Description = product.Product.Description,
                        OriginalPrice = product.Product.OriginalPrice,
                        DiscountPercentage = product.Product.DiscountPercentage,
                        DiscountAmount = product.Product.DiscountAmount,
                        StockQuantity = product.Product.StockQuantity,
                        AverageRating = product.Product.AverageRating,
                        TotalReviews = product.Product.TotalReviews,
                        IsFeatured = product.Product.IsFeatured,
                        ProductCategories = product.Product.ProductCategories,
                        Brand = product.Product.Brand,
                        Thumbnail = product.Product.Thumbnail,
                    });

            if (!string.IsNullOrEmpty(inData.Search))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(inData.Search));
            }

            if (inData.MinPrice.HasValue)
            {
                productQuery = productQuery.Where(p => p.OriginalPrice >= inData.MinPrice);
            }

            if (inData.MaxPrice.HasValue)
            {
                productQuery = productQuery.Where(p => p.OriginalPrice <= inData.MaxPrice);
            }

            if (inData.InStock.HasValue)
            {
                if (inData.InStock == true)
                {
                    productQuery = productQuery.Where(p => p.StockQuantity > 0);
                }
                else 
                {
                    productQuery = productQuery.Where(p => p.StockQuantity <= 0);
                }
            }

            if (inData.productCategoriesIds != null && inData.productCategoriesIds.Length > 0 )
            {
                productQuery = productQuery.Where(p => inData.productCategoriesIds.Contains(p.ProductCategoriesId));
            }

            if (inData.BrandIds != null && inData.BrandIds.Length > 0)
            {
                productQuery = productQuery.Where(p => inData.BrandIds.Contains(p.BrandId));
            }

            if (!string.IsNullOrEmpty(inData.Sort))
            {
                if (inData.Sort.ToLower() == "price_htl")
                {
                    productQuery = productQuery.OrderByDescending(p => p.OriginalPrice);
                }
                if (inData.Sort.ToLower() == "price_lth")
                {
                    productQuery = productQuery.OrderBy(p => p.OriginalPrice);
                }
                if (inData.Sort.ToLower() == "featured")
                {
                    productQuery = productQuery.OrderByDescending(p => p.IsFeatured);
                }
                if (inData.Sort.ToLower() == "rating")
                {
                    productQuery = productQuery.OrderBy(p => p.AverageRating);
                }
                if (inData.Sort.ToLower() == "newest")
                {
                    productQuery = productQuery.OrderByDescending(p => p.CreatedDate);
                }
            }

            return new ProductPagination()
            {
                PageIndex = inData.PageIndex,
                PageSize = inData.PageSize,
                Data = await productQuery
                    .Skip((inData.PageIndex - 1) * inData.PageSize)
                    .Take(inData.PageSize)
                    .ToListAsync(),
                Count = await _context.Products.CountAsync(),
                MinPrice = await _context.Products.MinAsync(p => p.OriginalPrice),
                MaxPrice = await _context.Products.MaxAsync(p => p.OriginalPrice),

            };
        }
    }
}

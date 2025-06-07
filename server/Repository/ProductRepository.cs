using Microsoft.EntityFrameworkCore;
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

        public async Task<Pagination<Product>> GetAllIncludingChildEntities(CatalogSpec inData)
        {
            IQueryable<Product> productQuery = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(inData.search))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(inData.search));
            }

            if (inData.minPrice.HasValue)
            {
                productQuery = productQuery.Where(p => p.Price >= inData.minPrice);
            }

            if (inData.maxPrice.HasValue)
            {
                productQuery = productQuery.Where(p => p.Price <= inData.maxPrice);
            }

            if (inData.inStock.HasValue)
            {
                if (inData.inStock == true)
                {
                    productQuery = productQuery.Where(p => p.StockQuantity > 0);
                }
                else 
                {
                    productQuery = productQuery.Where(p => p.StockQuantity <= 0);
                }
            }

            if (inData.productCategoriesId.HasValue)
            {
                productQuery = productQuery.Where(p => p.ProductCategoriesId == inData.productCategoriesId);
            }

            if (inData.brandId.HasValue)
            {
                productQuery = productQuery.Where(p => p.BrandId == inData.productCategoriesId);
            }

            if (!string.IsNullOrEmpty(inData.sort))
            {
                if (inData.sort.ToLower() == "price")
                {
                    if (inData.sortOrder.ToLower() == "des")
                    {
                        productQuery = productQuery.OrderByDescending(p => p.Price);
                    }
                    else
                    {
                        productQuery = productQuery.OrderBy(p => p.Price);
                    }
                }

                if (inData.sort == "name")
                {
                    if (inData.sortOrder.ToLower() == "des")
                    {
                        productQuery = productQuery.OrderByDescending(p => p.Name);
                    }
                    else
                    {
                        productQuery = productQuery.OrderBy(p => p.Name);
                    }
                }
            }

            return new Pagination<Product>()
            {
                PageIndex = inData.pageIndex,
                PageSize = inData.pageSize,
                Count = await _context.Products.CountAsync(),
                Data = await productQuery
                    .Skip((inData.pageIndex - 1) * inData.pageSize)
                    .Take(inData.pageSize)
                    .ToListAsync()
            };
        }
    }
}

using Microsoft.EntityFrameworkCore.Storage;
using server.Dto;
using Product = server.Entities.Product;

namespace server.Interface.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<ProductPagination> GetAllIncludingChildEntities(CatalogSpec inData);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<Product> GetByIdAsync(int id, bool includeDetails = false);
    }
}

using server.Dto;
using Product = server.Entities.Product;

namespace server.Interface.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<ProductPagination> GetAllIncludingChildEntities(CatalogSpec inData);
    }
}

using server.Dto;
using Product = server.Entities.Product;

namespace server.Interface.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Pagination<Product>> GetAllIncludingChildEntities(CatalogSpec inData);
    }
}

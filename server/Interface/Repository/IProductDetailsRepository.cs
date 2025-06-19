using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using server.Entities;

namespace server.Interface.Repository
{
    public interface IProductDetailsRepository : IGenericRepository<ProductDetails>
    {
        Task<ProductDetails> GetByProductIdAsync(int productId);
    }
}

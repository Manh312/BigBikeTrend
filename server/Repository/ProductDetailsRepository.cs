using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Entities;
using server.Interface.Repository;

namespace server.Repository
{
    public class ProductDetailsRepository : GenericRepository<ProductDetails>, IProductDetailsRepository
    {
        private readonly DataContext _context;

        public ProductDetailsRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ProductDetails> GetByProductIdAsync(int productId)
        {
            return await _context.ProductDetails
                .FirstOrDefaultAsync(pd => pd.ProductId == productId);
        }
    }
}
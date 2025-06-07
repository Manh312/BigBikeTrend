using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Entities;
using server.Interface.Repository;

namespace server.Repository
{
    public class ProductCategoriesRepository : GenericRepository<ProductCategories>, IProductCategoriesRepository
    {
        private readonly DataContext _context;
        public ProductCategoriesRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<ProductCategories>> GetAllIncludingImage()
        {
            return await _context.ProductCategories.Include(b => b.Image).ToListAsync();
        }
    }
}

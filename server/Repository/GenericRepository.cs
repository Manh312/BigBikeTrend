using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Entities; // Thêm namespace cho Product
using server.Interface.Repository;

namespace server.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DataContext context)
        {
            this._context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id, bool includeDetails = false)
        {
            var query = _dbSet.AsQueryable();

            // Kiểm tra và áp dụng include cho Product
            if (typeof(T) == typeof(Product))
            {
                var productQuery = query as IQueryable<Product>;
                if (productQuery != null)
                {
                    if (includeDetails)
                    {
                        query = productQuery
                            .Include(p => p.ProductCategories)
                            .Include(p => p.Brand)
                            .Include(p => p.Thumbnail)
                            .Include(p => p.ProductDetails) as IQueryable<T>;
                    }
                    else
                    {
                        query = productQuery
                            .Include(p => p.ProductCategories)
                            .Include(p => p.Brand)
                            .Include(p => p.Thumbnail) as IQueryable<T>;
                    }
                }
            }

            // Truy cập Id một cách an toàn
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
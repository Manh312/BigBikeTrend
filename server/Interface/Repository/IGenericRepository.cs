namespace server.Interface.Repository
{
    public interface IGenericRepository<T> where T : class
    {
            Task<IEnumerable<T>> GetAllAsync();
            Task<T?> GetByIdAsync(int id, bool includeDetails = false); // Thêm tham số includeDetails
            Task<T> AddAsync(T entity);
            Task<T> UpdateAsync(T entity);
            Task DeleteAsync(T entity);
    }
}

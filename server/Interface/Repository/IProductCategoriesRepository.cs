using server.Entities;

namespace server.Interface.Repository
{
    public interface IProductCategoriesRepository : IGenericRepository<ProductCategories>
    {
        Task<IEnumerable<ProductCategories>> GetAllIncludingImage();
    }
}

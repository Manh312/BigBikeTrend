using server.Dto;
using server.Entities;
using Product = server.Entities.Product;

namespace server.Interface.Service
{
    public interface ICatalogService
    {
        // Product
        Task<Pagination<Product>> GetAllProducts(CatalogSpec inData);
        Task<Product> CreateProduct(CreateProductReq inData);
        Task DeleteProduct(int productId);

        // Brand
        Task<IEnumerable<Brand>> GetAllBrand();
        Task<Brand> CreateBrand(CreateBrandReq inData);
        Task DeleteBrand(int brandId);

        // ProductCategories
        Task<IEnumerable<ProductCategories>> GetAllProductCategories();
        Task<ProductCategories> CreateProductCategories(CreateProductCategoriesReq inData);
        Task DeleteProductCategories(int productCategoriesId);
    }
}

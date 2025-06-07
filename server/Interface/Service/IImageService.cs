using server.Entities;

namespace server.Interface.Services
{
    public interface IImageService
    {
        Task<Image> SaveImageAsync(IFormFile file);
        Task DeleteImageAsync(int Id);
    }
}

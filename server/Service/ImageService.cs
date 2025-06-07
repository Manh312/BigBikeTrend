using server.Entities;
using server.Interface.Repository;
using server.Interface.Services;

namespace server.Service
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IImageRepository _imageRepository;

        public ImageService(IWebHostEnvironment environment, IImageRepository imageRepository ) 
        {
            _environment = environment;
            _imageRepository = imageRepository;
        }
        public async Task DeleteImageAsync(int Id)
        {
            Image image = await _imageRepository.GetByIdAsync( Id );
            if (image == null)
            {
                throw new ArgumentNullException($"No image found with id {Id}");
            }

            var contentPath = _environment.ContentRootPath;
            var imagePath = Path.Combine(contentPath, "Uploads");
            var fieldNameWithPath = Path.Combine(imagePath, image.ImageUrl);

            if (Directory.Exists(fieldNameWithPath))
            {
                Directory.Delete(fieldNameWithPath);
            }
            await _imageRepository.DeleteAsync(image);
        }

        public async Task<Image> SaveImageAsync(IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("File is empty");
            }
            var contentPath = _environment.ContentRootPath;
            var imagePath = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            var imageName = file.FileName;
            var imageExt = Path.GetExtension(imageName);
            var fileName = $"{Guid.NewGuid().ToString()}{imageExt}";

            var fileNameWithPath = Path.Combine(imagePath, fileName);

            using var fileStream = new FileStream(fileNameWithPath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            Image image = new Image()
            {
                ImageUrl = fileName,
                ImageName = imageName,
                ImageNameExt = imageExt
            };

            return await _imageRepository.AddAsync(image);
        }
    }
}

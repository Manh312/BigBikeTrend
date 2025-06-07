using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Entities;
using server.Interface.Services;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ImageController(IImageService imageService, IMapper mapper) 
        {
            this._imageService = imageService;
            this._mapper = mapper;
        }
        
        [HttpPost]
        public async Task<ActionResult<Image>> UploadImage(IFormFile file)
        {
            Image image = await _imageService.SaveImageAsync(file);
            return Ok(_mapper.Map<ImageDtoRes>(image));
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Image>> DeleteImage(int Id)
        {
            await _imageService.DeleteImageAsync(Id);

            return Ok();
        }
    }
}

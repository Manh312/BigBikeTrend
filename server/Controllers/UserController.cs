using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Interface.Repository;
using server.Repository;

namespace server.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<ResponseDto>> GetAll()
        {
            ResponseDto responseDto = new ResponseDto();
            responseDto.Data = await this._userRepository.GetAllUsers();
            return Ok(responseDto);
        }
    }
}

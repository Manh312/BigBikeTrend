using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Entities;
using server.Helper;
using server.Interface.Repository;

namespace server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHelper _jwtHelper;
        public AuthController(IUserRepository userRepository, IJwtHelper helper)
        {
            this._userRepository = userRepository;
            this._jwtHelper = helper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ResponseDto>> Login([FromBody] LoginUserReqDto req)
        {
            User? user = await this._userRepository.GetUserByEmail(req.Email);
            ResponseDto res = new ResponseDto();

            if (user == null)
            {
                res.IsSuccessed = false;
                res.Message = "Invalid Credentials!";
                return BadRequest(res);
            }

            if (!BCrypt.Net.BCrypt.Verify(req.Password, user.Password))
            {
                res.IsSuccessed = false;
                res.Message = "Invalid Credentials!";
                return BadRequest(res);
            }
            var refreshToken = _jwtHelper.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpire = DateTime.Now.AddDays(2);

            await _userRepository.UpdateUser(user);

            LoginUserResDto userDetail = new LoginUserResDto()
            {
                AccessToken = this._jwtHelper.GenerateJwtToken(user),
                RefreshToken = refreshToken
            };

            res.Data = userDetail;

            return Ok(res);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ResponseDto>> Register([FromBody] RegisterUserReqDto req)
        {
            User? user = await this._userRepository.GetUserByEmail(req.Email);
            ResponseDto res = new ResponseDto();
            if (user != null)
            {
                res.IsSuccessed = false;
                res.Message = $"User with email {req.Email} already exsist";
                return BadRequest(res);
            }

            User newUser = new User()
            {
                UserName = req.UserName,
                Email = req.Email,
                Address = req.Address,
                Password = BCrypt.Net.BCrypt.HashPassword(req.Password)
            };
            bool result = await this._userRepository.AddUser(newUser);
            if (!result)
            {
                res.IsSuccessed = false;
                res.Message = "User registration failed!";
                return BadRequest(res);
            }
            res.Message = "User registered successfully!";
            return Ok(res);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<ResponseDto>> RefreshToken([FromBody] RefreshTokenDto req)
        {
            ResponseDto res = new ResponseDto();
            if (req.RefreshToken == null || req.AccessToken == null)
            {
                res.IsSuccessed = false;
                res.Message = "Invalid request!";
                return BadRequest(res);
            }
            var refreshToken = req.RefreshToken;
            var accessToken = req.AccessToken;

            Console.WriteLine($"AccessToken: {accessToken}");
            Console.WriteLine($"RefreshToken: {refreshToken}");

            var principal = _jwtHelper.GetPrincipalFromExpiredToken(accessToken);
            //var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            var email = principal.Identity.Name;
            Console.WriteLine($"Email from token: {email}");

            User? user = await this._userRepository.GetUserByEmail(email);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpire <= DateTime.Now)
            {
                res.IsSuccessed = false;
                res.Message = "Invalid request!";
                return BadRequest(res);
            }

            refreshToken = _jwtHelper.GenerateRefreshToken();
            accessToken = _jwtHelper.GenerateJwtToken(user);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpire = DateTime.Now.AddDays(2);

            await _userRepository.UpdateUser(user);
            RefreshTokenDto data = new RefreshTokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            res.Data = data;

            return Ok(res);
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<ActionResult<ResponseDto>> Revoke()
        {
            ResponseDto res = new ResponseDto();
            var email = User.Identity.Name;
            User? user = await this._userRepository.GetUserByEmail(email);
            if (user == null)
            {
                res.IsSuccessed = false;
                res.Message = "Invalid request!";
                return BadRequest(res);
            }
            user.RefreshToken = "";
            await this._userRepository.UpdateUser(user);

            res.Message = "User Successfully logged out!";
            return Ok(res);

        }
    }
}

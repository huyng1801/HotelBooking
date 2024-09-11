using HotelBookingAPI.DTO;
using HotelBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            var userDTO = _authService.Authenticate(login);
            if (userDTO == null)
            {
                return Unauthorized();
            }

            var token = _authService.GenerateToken(userDTO);
            var refreshToken = _authService.GenerateRefreshToken(userDTO.Username);

            return Ok(new
            {
                userDTO.UserId,
                userDTO.Username,
                Token = token,
                RefreshToken = refreshToken.Token,
                userDTO.Role
            });
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var tokens = _authService.RefreshToken(request.AccessToken, request.RefreshToken);
            if (tokens.Token == null || tokens.RefreshToken == null)
            {
                return Unauthorized("Invalid refresh token");
            }

            var userDTO = _authService.GetUserFromExpiredToken(request.AccessToken);

            return Ok(new
            {
                userDTO.UserId,
                userDTO.Username,
                tokens.Token,
                tokens.RefreshToken,
                userDTO.Role
            });
        }
    }
}

﻿using System.Linq;
using HotelBookingAPI.Data;
using HotelBookingAPI.DTO;

namespace HotelBookingAPI.Services
{
    public interface IAuthService
    {
        UserDTO Authenticate(LoginDTO login);
        string GenerateToken(UserDTO userDTO);
        RefreshToken GenerateRefreshToken(string Username);
        bool ValidateRefreshToken(string token);
        UserDTO GetUserFromExpiredToken(string token);
        (string Token, string RefreshToken) RefreshToken(string accessToken, string refreshToken);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public UserDTO Authenticate(LoginDTO login)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == login.Username);
            if (user == null || !PasswordHashService.VerifyPasswordHash(login.Password, user.HashPassword))
            {
                return null;
            }

            return new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public string GenerateToken(UserDTO userDTO)
        {
            return _tokenService.GenerateToken(userDTO);
        }

        public RefreshToken GenerateRefreshToken(string Username)
        {
            return _tokenService.GenerateRefreshToken(Username);
        }

        public bool ValidateRefreshToken(string token)
        {
            return _tokenService.ValidateRefreshToken(token);
        }

        public UserDTO GetUserFromExpiredToken(string token)
        {
            return _tokenService.GetUserFromExpiredToken(token);
        }

        public (string Token, string RefreshToken) RefreshToken(string accessToken, string refreshToken)
        {
            return _tokenService.RefreshToken(accessToken, refreshToken);
        }


    }
}

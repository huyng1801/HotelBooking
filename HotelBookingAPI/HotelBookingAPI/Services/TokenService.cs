using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HotelBookingAPI.Data;
using HotelBookingAPI.DTO;
using HotelBookingAPI.Middleware;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HotelBookingAPI.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserDTO userDTO);
        RefreshToken GenerateRefreshToken(string Username);
        bool ValidateRefreshToken(string token);
        UserDTO GetUserFromExpiredToken(string token);
        (string Token, string RefreshToken) RefreshToken(string accessToken, string refreshToken);
    }

    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationDbContext _context;

        public TokenService(IOptions<JwtSettings> jwtSettings, ApplicationDbContext context)
        {
            _jwtSettings = jwtSettings.Value;
            _context = context;
        }

        public string GenerateToken(UserDTO userDTO)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userDTO.Username),
                new Claim(JwtRegisteredClaimNames.Email, userDTO.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userDTO.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(string Username)
        {
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid().ToString(),
                Username = Username,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };

            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();

            return refreshToken;
        }

        public bool ValidateRefreshToken(string token)
        {
            var refreshToken = _context.RefreshTokens.SingleOrDefault(rt => rt.Token == token);
            if (refreshToken == null || !refreshToken.IsActive)
            {
                return false;
            }

            return true;
        }

        public UserDTO GetUserFromExpiredToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false, // We want to check the expired token
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key))
                }, out SecurityToken securityToken);

                var jwtToken = securityToken as JwtSecurityToken;
                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                // Extract claims directly from jwtToken
                var Username = jwtToken.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
                var email = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;
                var roleClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(roleClaim))
                {
                    throw new SecurityTokenException("Invalid token claims");
                }

                // Retrieve the user from the database
                var user = _context.Users.SingleOrDefault(u => u.Username == Username);
                if (user == null)
                {
                    throw new SecurityTokenException("User not found");
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
            catch (Exception ex)
            {
                // Log exception if necessary
                Debug.WriteLine($"Error: {ex.Message}");
                throw new SecurityTokenException("Token validation failed", ex);
            }
        }


        public (string Token, string RefreshToken) RefreshToken(string accessToken, string refreshToken)
        {
            if (!ValidateRefreshToken(refreshToken))
            {
                return (null, null);
            }

            var userDTO = GetUserFromExpiredToken(accessToken);

            var newJwtToken = GenerateToken(userDTO);
            var newRefreshToken = GenerateRefreshToken(userDTO.Username);

            var existingToken = _context.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken);
            if (existingToken != null)
            {
                existingToken.Revoked = DateTime.UtcNow;
                _context.RefreshTokens.Update(existingToken);
                _context.SaveChanges();
            }

            return (newJwtToken, newRefreshToken.Token);
        }
    }
}

using HotelBookingAPI.DTO;
using HotelBookingAPI.Data;
using HotelBookingAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var userDTOs = new List<UserDTO>();
            foreach (var user in users)
            {
                userDTOs.Add(new UserDTO
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    BirthDate = user.BirthDate,
                    Country = user.Country,
                    Role = user.Role,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                });
            }
            return Ok(userDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Country = user.Country,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] UserRequestDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserId = Guid.NewGuid(), 
                Username = userDTO.Username,
                FullName = userDTO.FullName,
                PhoneNumber = userDTO.PhoneNumber,
                Email = userDTO.Email,
                BirthDate = userDTO.BirthDate,
                Country = userDTO.Country,
                HashPassword = userDTO.HashPassword,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
                Role = (int)userDTO.Role
            };

            var result = await _userService.InsertAsync(user);
            if (!result)
            {
                return BadRequest("Could not create the user.");
            }

            var userResponseDTO = new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Country = user.Country,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, userResponseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserRequestDTO userDTO)
        {
            var user = new User
            {
                UserId = id,
                FullName = userDTO.FullName,
                PhoneNumber = userDTO.PhoneNumber,
                Email = userDTO.Email,
                BirthDate = userDTO.BirthDate,
                Country = userDTO.Country,
                HashPassword = userDTO.HashPassword
            };

            var result = await _userService.UpdateAsync(user);
            if (!result)
            {
                return BadRequest("Could not update the user.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
            {
                return NotFound("User not found.");
            }

            return NoContent();
        }

        [HttpPut("toggleIsActive/{id}")]
        public async Task<IActionResult> ToggleIsActive(Guid id)
        {
            var result = await _userService.ToggleIsActiveAsync(id);
            if (!result)
            {
                return BadRequest("Could not toggle user active status.");
            }

            return NoContent();
        }
    }
}

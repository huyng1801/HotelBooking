using HotelBookingAPI.DTO;
using HotelBookingAPI.Data;
using HotelBookingAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _roomService.GetAllAsync();
            var roomDTOs = rooms.Select(room => new RoomDTO
            {
                RoomId = room.RoomId,
                RoomName = room.RoomName,
                Price = room.Price,
                RoomCount = room.RoomCount,
                Area = room.Area,
                NumberPerson = room.NumberPerson,
                View = room.View,
                EatBreakfast = room.EatBreakfast,
                BedDescription = room.BedDescription,
                CreatedAt = room.CreatedAt,
                UpdatedAt = room.UpdatedAt,
                ImageUrls = room.RoomImages.Select(img => new RoomImageDTO
                {
                    RoomImageId = img.RoomImageId,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/Images/Rooms/{img.ImageUrl}",
                    CreatedAt = img.CreatedAt
                }).ToList()
            }).ToList();

            return Ok(roomDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var room = await _roomService.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            var roomDTO = new RoomDTO
            {
                RoomId = room.RoomId,
                RoomName = room.RoomName,
                Price = room.Price,
                RoomCount = room.RoomCount,
                Area = room.Area,
                NumberPerson = room.NumberPerson,
                View = room.View,
                EatBreakfast = room.EatBreakfast,
                BedDescription = room.BedDescription,
                CreatedAt = room.CreatedAt,
                UpdatedAt = room.UpdatedAt,
                ImageUrls = room.RoomImages.Select(img => new RoomImageDTO
                {
                    RoomImageId = img.RoomImageId,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/Images/Rooms/{img.ImageUrl}",
                    CreatedAt = img.CreatedAt
                }).ToList()
            };

            return Ok(roomDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] RoomRequstDTO roomDTO, [FromForm] List<IFormFile> files)
        {
            var room = new Room
            {
                RoomId = Guid.NewGuid(),
                RoomName = roomDTO.RoomName,
                Price = roomDTO.Price,
                RoomCount = roomDTO.RoomCount,
                Area = roomDTO.Area,
                NumberPerson = roomDTO.NumberPerson,
                View = roomDTO.View,
                EatBreakfast = roomDTO.EatBreakfast,
                BedDescription = roomDTO.BedDescription,
                HotelId = roomDTO.HotelId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Convert List<IFormFile> to IFormFileCollection
            var formFileCollection = new FormFileCollection();
            formFileCollection.AddRange(files);

            var result = await _roomService.InsertAsync(room, formFileCollection);
            if (!result)
            {
                return BadRequest("Unable to insert room.");
            }

            var createdRoomDTO = new RoomDTO
            {
                RoomId = room.RoomId,
                RoomName = room.RoomName,
                Price = room.Price,
                RoomCount = room.RoomCount,
                Area = room.Area,
                NumberPerson = room.NumberPerson,
                View = room.View,
                EatBreakfast = room.EatBreakfast,
                BedDescription = room.BedDescription,
                CreatedAt = room.CreatedAt,
                UpdatedAt = room.UpdatedAt,
                ImageUrls = room.RoomImages.Select(img => new RoomImageDTO
                {
                    RoomImageId = img.RoomImageId,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/Images/Rooms/{img.ImageUrl}",
                    CreatedAt = img.CreatedAt
                }).ToList()
            };

            return CreatedAtAction(nameof(GetById), new { id = createdRoomDTO.RoomId }, createdRoomDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] RoomRequstDTO roomDTO, [FromForm] List<IFormFile> files)
        {
            var room = new Room
            {
                RoomId = id,
                RoomName = roomDTO.RoomName,
                Price = roomDTO.Price,
                RoomCount = roomDTO.RoomCount,
                Area = roomDTO.Area,
                NumberPerson = roomDTO.NumberPerson,
                View = roomDTO.View,
                EatBreakfast = roomDTO.EatBreakfast,
                BedDescription = roomDTO.BedDescription,
                HotelId = roomDTO.HotelId,
                UpdatedAt = DateTime.UtcNow
            };

            // Convert List<IFormFile> to IFormFileCollection
            var formFileCollection = new FormFileCollection();
            formFileCollection.AddRange(files);

            var result = await _roomService.UpdateAsync(room, formFileCollection);
            if (!result)
            {
                return BadRequest("Unable to update room.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _roomService.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Room not found.");
            }

            return NoContent();
        }
    }
}

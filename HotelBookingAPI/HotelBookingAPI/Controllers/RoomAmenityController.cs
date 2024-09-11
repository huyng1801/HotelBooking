using HotelBookingAPI.DTO;
using HotelBookingAPI.Data;
using HotelBookingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RoomAmenityController : ControllerBase
    {
        private readonly IRoomAmenityService _roomAmenityService;

        public RoomAmenityController(IRoomAmenityService roomAmenityService)
        {
            _roomAmenityService = roomAmenityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomAmenityDTO>>> GetAll()
        {
            var roomAmenities = await _roomAmenityService.GetAllAsync();
            var roomAmenityDTOs = new List<RoomAmenityDTO>();
            foreach (var roomAmenity in roomAmenities)
            {
                roomAmenityDTOs.Add(new RoomAmenityDTO
                {
                    RoomAmenityId = roomAmenity.RoomAmenityId,
                    RoomId = roomAmenity.RoomId,
                    AmenityId = roomAmenity.AmenityId
                });
            }
            return Ok(roomAmenityDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomAmenityDTO>> GetById(int id)
        {
            var roomAmenity = await _roomAmenityService.GetByIdAsync(id);
            if (roomAmenity == null)
            {
                return NotFound();
            }

            var roomAmenityDTO = new RoomAmenityDTO
            {
                RoomAmenityId = roomAmenity.RoomAmenityId,
                RoomId = roomAmenity.RoomId,
                AmenityId = roomAmenity.AmenityId
            };

            return Ok(roomAmenityDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(RoomAmenityDTO roomAmenityDTO)
        {
            var roomAmenity = new RoomAmenity
            {
                RoomId = roomAmenityDTO.RoomId,
                AmenityId = roomAmenityDTO.AmenityId
            };

            var result = await _roomAmenityService.InsertAsync(roomAmenity);
            if (!result)
            {
                return BadRequest("Unable to insert room amenity.");
            }

            return CreatedAtAction(nameof(GetById), new { id = roomAmenity.RoomAmenityId }, roomAmenityDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, RoomAmenityDTO roomAmenityDTO)
        {
            var roomAmenity = new RoomAmenity
            {
                RoomAmenityId = id,
                RoomId = roomAmenityDTO.RoomId,
                AmenityId = roomAmenityDTO.AmenityId
            };

            var result = await _roomAmenityService.UpdateAsync(roomAmenity);
            if (!result)
            {
                return BadRequest("Unable to update room amenity.");
            }

            return NoContent();
        }
        [HttpGet("ByRoom/{roomId}")]
        public async Task<ActionResult<IEnumerable<RoomAmenityDTO>>> GetByRoomId(Guid roomId)
        {
            var roomAmenities = await _roomAmenityService.GetByRoomIdAsync(roomId);
            if (roomAmenities == null)
            {
                return NotFound();
            }

            var roomAmenityDTOs = new List<RoomAmenityDTO>();
            foreach (var roomAmenity in roomAmenities)
            {
                roomAmenityDTOs.Add(new RoomAmenityDTO
                {
                    RoomAmenityId = roomAmenity.RoomAmenityId,
                    RoomId = roomAmenity.RoomId,
                    AmenityId = roomAmenity.AmenityId,
                    RoomName = roomAmenity.Room.RoomName,
                    AmenityName = roomAmenity.Amenity.AmenityName
                });
            }
            return Ok(roomAmenityDTOs);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _roomAmenityService.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Room amenity not found.");
            }

            return NoContent();
        }
    }
}

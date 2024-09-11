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
    //[Authorize]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityService _amenityService;

        public AmenityController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAll()
        {
            var amenities = await _amenityService.GetAllAsync();
            var amenityDTOs = new List<AmenityDTO>();
            foreach (var amenity in amenities)
            {
                amenityDTOs.Add(new AmenityDTO
                {
                    AmenityId = amenity.AmenityId,
                    AmenityName = amenity.AmenityName
                });
            }
            return Ok(amenityDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDTO>> GetByID(Guid id)
        {
            var amenity = await _amenityService.GetByIDAsync(id);
            if (amenity == null)
            {
                return NotFound();
            }

            var amenityDTO = new AmenityDTO
            {
                AmenityId = amenity.AmenityId,
                AmenityName = amenity.AmenityName
            };

            return Ok(amenityDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromForm] AmenityRequestDTO amenityDTO)
        {
            var amenity = new Amenity
            {
                AmenityId = Guid.NewGuid(),
                AmenityName = amenityDTO.AmenityName
            };

            var result = await _amenityService.InsertAsync(amenity);
            if (!result)
            {
                return BadRequest("Unable to insert amenity.");
            }

            var createdAmenityDTO = new AmenityDTO
            {
                AmenityId = amenity.AmenityId,
                AmenityName = amenity.AmenityName
            };

            return CreatedAtAction(nameof(GetByID), new { id = createdAmenityDTO.AmenityId }, createdAmenityDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromForm] AmenityRequestDTO amenityDTO)
        {
            var amenity = new Amenity
            {
                AmenityId = id,
                AmenityName = amenityDTO.AmenityName
            };

            var result = await _amenityService.UpdateAsync(amenity);
            if (!result)
            {
                return BadRequest("Unable to update amenity.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _amenityService.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Amenity not found.");
            }

            return NoContent();
        }
    }
}

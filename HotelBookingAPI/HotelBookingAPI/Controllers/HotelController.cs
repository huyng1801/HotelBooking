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
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hotels = await _hotelService.GetAllAsync();
            var hotelDTOs = new List<HotelDTO>();
            foreach (var hotel in hotels)
            {
                var hotelImages = new List<HotelImageDTO>();
                foreach (var hotelImage in hotel.HotelImages)
                {
                    hotelImages.Add(new HotelImageDTO
                    {
                        HotelImageId = hotelImage.HotelImageId,
                        ImageUrl = $"{Request.Scheme}://{Request.Host}/Images/Hotels/{hotelImage.ImageUrl}",
                        CreatedAt = hotelImage.CreatedAt
                    });
                }

                hotelDTOs.Add(new HotelDTO
                {
                    HotelId = hotel.HotelId,
                    HotelName = hotel.HotelName,
                    City = hotel.City,
                    PhoneNumber = hotel.PhoneNumber,
                    Address = hotel.Address,
                    AccommodationPolicy = hotel.AccommodationPolicy,
                    Description = hotel.Description,
                    StarRating = hotel.Star,
                    IsActive = hotel.IsActive,
                    CreatedAt = hotel.CreatedAt,
                    UpdatedAt = hotel.UpdatedAt,
                    Images = hotelImages
                });
            }
            return Ok(hotelDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var hotel = await _hotelService.GetByIDAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            var hotelDTO = new HotelDTO
            {
                HotelId = hotel.HotelId,
                HotelName = hotel.HotelName,
                City = hotel.City,
                PhoneNumber = hotel.PhoneNumber,
                Address = hotel.Address,
                AccommodationPolicy = hotel.AccommodationPolicy,
                Description = hotel.Description,
                StarRating = hotel.Star,
                IsActive = hotel.IsActive,
                CreatedAt = hotel.CreatedAt,
                UpdatedAt = hotel.UpdatedAt,
                Images = new List<HotelImageDTO>()
            };

            foreach (var hotelImage in hotel.HotelImages)
            {
                hotelDTO.Images.Add(new HotelImageDTO
                {
                    HotelImageId = hotelImage.HotelImageId,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/Images/Hotels/{hotelImage.ImageUrl}",
                    CreatedAt = hotelImage.CreatedAt
                });
            }

            return Ok(hotelDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] HotelRequestDTO hotelDTO, [FromForm] List<IFormFile> files)
        {
            var hotel = new Hotel
            {
                HotelName = hotelDTO.HotelName,
                City = hotelDTO.City,
                PhoneNumber = hotelDTO.PhoneNumber,
                Address = hotelDTO.Address,
                AccommodationPolicy = hotelDTO.AccommodationPolicy,
                Description = hotelDTO.Description,
                Star = hotelDTO.Star,
                IsActive = hotelDTO.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Convert List<IFormFile> to IFormFileCollection
            var formFileCollection = new FormFileCollection();
            formFileCollection.AddRange(files);

            var result = await _hotelService.InsertAsync(hotel, formFileCollection);
            if (!result)
            {
                return BadRequest("Unable to insert hotel.");
            }

            var createdHotelDTO = new HotelDTO
            {
                HotelId = hotel.HotelId,
                HotelName = hotel.HotelName,
                City = hotel.City,
                PhoneNumber = hotel.PhoneNumber,
                Address = hotel.Address,
                AccommodationPolicy = hotel.AccommodationPolicy,
                Description = hotel.Description,
                StarRating = hotel.Star,
                IsActive = hotel.IsActive,
                CreatedAt = hotel.CreatedAt,
                UpdatedAt = hotel.UpdatedAt,
                Images = hotel.HotelImages.Select(image => new HotelImageDTO
                {
                    HotelImageId = image.HotelImageId,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/Images/Hotels/{image.ImageUrl}",
                    CreatedAt = image.CreatedAt
                }).ToList()
            };

            return CreatedAtAction(nameof(GetByID), new { id = createdHotelDTO.HotelId }, createdHotelDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] HotelRequestDTO hotelDTO, [FromForm] List<IFormFile> files)
        {
            var hotel = new Hotel
            {
                HotelId = id,
                HotelName = hotelDTO.HotelName,
                City = hotelDTO.City,
                PhoneNumber = hotelDTO.PhoneNumber,
                Address = hotelDTO.Address,
                AccommodationPolicy = hotelDTO.AccommodationPolicy,
                Description = hotelDTO.Description,
                Star = hotelDTO.Star,
                IsActive = hotelDTO.IsActive,
                UpdatedAt = DateTime.UtcNow
            };

            // Convert List<IFormFile> to IFormFileCollection
            var formFileCollection = new FormFileCollection();
            formFileCollection.AddRange(files);

            var result = await _hotelService.UpdateAsync(hotel, formFileCollection);
            if (!result)
            {
                return BadRequest("Unable to update hotel.");
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _hotelService.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Hotel not found.");
            }

            return NoContent();
        }

        [HttpGet("{hotelId}/rooms")]
        public async Task<IActionResult> GetRoomsByHotelId(Guid hotelId)
        {
            var rooms = await _hotelService.GetRoomsByHotelIdAsync(hotelId);
            var roomDTOs = new List<RoomDTO>();

            foreach (var room in rooms)
            {
                roomDTOs.Add(new RoomDTO
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
                    }).ToList(),
                    RoomAmenities = room.RoomAmenities.Select(amenity => new RoomAmenityDTO
                    {
                        RoomAmenityId = amenity.RoomAmenityId,
                        RoomId = amenity.RoomId,
                        AmenityId = amenity.AmenityId,
                        RoomName = amenity.Room.RoomName,
                        AmenityName = amenity.Amenity.AmenityName
                    }).ToList()
                });
            }

            return Ok(roomDTOs);
        }

    }
}

using HotelBookingAPI.DTO;
using HotelBookingAPI.Data;
using HotelBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        private readonly IRoomAmenityService _roomAmenityService;
        private readonly IBookingService _bookingService;
        private readonly IBookingDetailService _bookingDetailService;
        private readonly IVNPayService _vnpayService;
        private readonly IUserService _userService;

        public HomeController(
            IHotelService hotelService,
            IRoomService roomService,
            IBookingService bookingService,
            IBookingDetailService bookingDetailService,
            IRoomAmenityService roomAmenityService,
            IVNPayService vnpayService,
            IUserService userService)
        {
            _hotelService = hotelService;
            _roomService = roomService;
            _bookingService = bookingService;
            _bookingDetailService = bookingDetailService;
            _roomAmenityService = roomAmenityService;
            _vnpayService = vnpayService;
            _userService = userService;
        }


        [HttpGet("hotels")]
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

        [HttpGet("{id}/hotels")]
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

        [HttpGet("{hotelId}/rooms")]
        public async Task<IActionResult> GetRoomsByHotelId(Guid hotelId)
        {
            var rooms = await _hotelService.GetRoomsByHotelIdAsync(hotelId);
            var roomDTOs = new List<RoomDTO>();

            foreach (var room in rooms)
            {
                var roomImages = room.RoomImages.Select(img => new RoomImageDTO
                {
                    RoomImageId = img.RoomImageId,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/Images/Rooms/{img.ImageUrl}",
                    CreatedAt = img.CreatedAt
                }).ToList();

                var roomAmenities = room.RoomAmenities.Select(amenity => new RoomAmenityDTO
                {
                    RoomAmenityId = amenity.RoomAmenityId,
                    RoomId = amenity.RoomId,
                    AmenityId = amenity.AmenityId,
                    AmenityName = amenity.Amenity.AmenityName
                }).ToList();

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
                    ImageUrls = roomImages,
                    RoomAmenities = roomAmenities
                });
            }

            return Ok(roomDTOs);
        }


        [HttpGet("rooms/{roomId}/amenities")]
        public async Task<ActionResult<IEnumerable<RoomAmenityDTO>>> GetRoomAmenities(Guid roomId)
        {
            var roomAmenities = await _roomAmenityService.GetByRoomIdAsync(roomId);
            if (roomAmenities == null)
            {
                return NotFound();
            }

            var roomAmenityDTOs = roomAmenities.Select(roomAmenity => new RoomAmenityDTO
            {
                RoomAmenityId = roomAmenity.RoomAmenityId,
                RoomId = roomAmenity.RoomId,
                AmenityId = roomAmenity.AmenityId,
                AmenityName = roomAmenity.Amenity.AmenityName
            }).ToList();

            return Ok(roomAmenityDTOs);
        }

        [HttpGet("bookings")]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllAsync();
            var bookingDTOs = bookings.Select(booking => new BookingDTO
            {
                BookingId = booking.BookingId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalAmount = booking.TotalAmount,
                Status = booking.Status,
                NumberOfAdults = booking.NumberOfAdults,
                NumberOfChildren = booking.NumberOfChildren,
                Note = booking.Note,
                PaymentMethod = booking.PaymentMethod,
                PaymentStatus = booking.PaymentStatus,
                CreatedAt = booking.CreatedAt,
                UpdatedAt = booking.UpdatedAt,
                UserId = booking.UserId,
                BookingDetails = booking.BookingDetails.Select(bd => new BookingDetailDTO
                {
                    BookingDetailId = bd.BookingDetailId,
                    BookingId = bd.BookingId,
                    RoomId = bd.RoomId,
                    UnitPrice = bd.UnitPrice,
                    RoomCount = bd.RoomCount
                }).ToList()
            }).ToList();

            return Ok(bookingDTOs);
        }

        [HttpGet("bookings/{id}")]
        public async Task<ActionResult<BookingDTO>> GetBookingById(string id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            var bookingDTO = new BookingDTO
            {
                BookingId = booking.BookingId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalAmount = booking.TotalAmount,
                Status = booking.Status,
                NumberOfAdults = booking.NumberOfAdults,
                NumberOfChildren = booking.NumberOfChildren,
                Note = booking.Note,
                PaymentMethod = booking.PaymentMethod,
                PaymentStatus = booking.PaymentStatus,
                CreatedAt = booking.CreatedAt,
                UpdatedAt = booking.UpdatedAt,
                UserId = booking.UserId,
                BookingDetails = booking.BookingDetails.Select(bd => new BookingDetailDTO
                {
                    BookingDetailId = bd.BookingDetailId,
                    BookingId = bd.BookingId,
                    RoomId = bd.RoomId,
                    UnitPrice = bd.UnitPrice,
                    RoomCount = bd.RoomCount
                }).ToList()
            };

            return Ok(bookingDTO);
        }

     

        [HttpPut("bookings/{id}")]
        public async Task<IActionResult> UpdateBooking(string id, [FromForm] BookingDTO bookingDTO)
        {
            if (id != bookingDTO.BookingId)
            {
                return BadRequest();
            }

            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            booking.CheckInDate = bookingDTO.CheckInDate;
            booking.CheckOutDate = bookingDTO.CheckOutDate;
            booking.TotalAmount = bookingDTO.TotalAmount;
            booking.Status = bookingDTO.Status;
            booking.NumberOfAdults = bookingDTO.NumberOfAdults;
            booking.NumberOfChildren = bookingDTO.NumberOfChildren;
            booking.Note = bookingDTO.Note;
            booking.PaymentMethod = bookingDTO.PaymentMethod;
            booking.PaymentStatus = bookingDTO.PaymentStatus;
            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingService.UpdateAsync(booking);
            return NoContent();
        }

        [HttpDelete("bookings/{id}")]
        public async Task<IActionResult> DeleteBooking(string id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            await _bookingService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("bookings")]
        public async Task<ActionResult> CreateBookingAndGeneratePayment([FromBody] BookingDTO bookingDTO)
        {

            // Create Booking
            var booking = new Booking
            {
                BookingId = bookingDTO.BookingId,
                CheckInDate = bookingDTO.CheckInDate,
                CheckOutDate = bookingDTO.CheckOutDate,
                TotalAmount = bookingDTO.TotalAmount,
                Status = bookingDTO.Status,
                NumberOfAdults = bookingDTO.NumberOfAdults,
                NumberOfChildren = bookingDTO.NumberOfChildren,
                Note = bookingDTO.Note,
                PaymentMethod = bookingDTO.PaymentMethod,
                PaymentStatus = bookingDTO.PaymentStatus,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = bookingDTO.UserId
            };

            var createdBooking = await _bookingService.CreateAsync(booking);

            if (bookingDTO.BookingDetails != null && bookingDTO.BookingDetails.Any())
            {
                foreach (var detailDTO in bookingDTO.BookingDetails)
                {
                    var bookingDetail = new BookingDetail
                    {
                        BookingId = createdBooking.BookingId,
                        RoomId = detailDTO.RoomId,
                        UnitPrice = detailDTO.UnitPrice,
                        RoomCount = detailDTO.RoomCount
                    };

                    await _bookingDetailService.CreateAsync(bookingDetail);
                }
            }

            // Generate Payment URL
            var returnUrl = Url.Action(nameof(VNPayReturn), "Home", null, Request.Scheme);
            var paymentUrl = _vnpayService.CreatePaymentUrl(bookingDTO, returnUrl);

            return CreatedAtAction(nameof(GetBookingById), new { id = createdBooking.BookingId }, new { bookingId = createdBooking.BookingId, paymentUrl });
        }


        [HttpGet("vnpay_return")]
        [AllowAnonymous]
        public async Task<IActionResult> VNPayReturn()
        {
            var queryString = Request.QueryString.Value;
            var vnp_SecureHash = Request.Query["vnp_SecureHash"];
            var inputData = string.Join("&", Request.Query
                .Where(q => q.Key != "vnp_SecureHash")
                .Select(q => $"{q.Key}={q.Value}"));

            var isValidSignature = _vnpayService.ValidateSignature(inputData, vnp_SecureHash);

            if (isValidSignature)
            {
                var bookingId = Request.Query["vnp_TxnRef"].ToString();
                await _bookingService.UpdateStatusAsync(bookingId, 2);
                return Ok("Payment successful.");
            }

            return BadRequest("Invalid payment signature.");
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchHotels(
           [FromQuery] string city,
           [FromQuery] DateTime checkInDate,
           [FromQuery] DateTime checkOutDate,
           [FromQuery] int numberOfPersons,
           [FromQuery] int numberOfRooms)
        {
            var hotels = await _hotelService.SearchHotelsAsync(city, city, checkInDate, checkOutDate, numberOfPersons, numberOfRooms);
            var hotelDTOs = hotels.Select(hotel => new HotelDTO
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
                Images = hotel.HotelImages.Select(img => new HotelImageDTO
                {
                    HotelImageId = img.HotelImageId,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/Images/Hotels/{img.ImageUrl}",
                    CreatedAt = img.CreatedAt
                }).ToList()
            }).ToList();

            return Ok(hotelDTOs);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDTO userDTO)
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
                Role = 2
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

            return Ok(userResponseDTO);
        }
    }
}

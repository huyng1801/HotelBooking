using HotelBookingAPI.DTO;
using HotelBookingAPI.Data;
using HotelBookingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IBookingDetailService _bookingDetailService;

        public BookingController(IBookingService bookingService, IBookingDetailService bookingDetailService)
        {
            _bookingService = bookingService;
            _bookingDetailService = bookingDetailService;
        }

        [HttpGet]
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
                BookingDetails = booking.BookingDetails != null ? booking.BookingDetails.Select(bd => new BookingDetailDTO
                {
                    BookingDetailId = bd.BookingDetailId,
                    BookingId = bd.BookingId,
                    RoomId = bd.RoomId,
                    UnitPrice = bd.UnitPrice,
                    RoomCount = bd.RoomCount,
                    RoomName = bd.Room.RoomName
                }).ToList() : new List<BookingDetailDTO>()
            }).ToList();

            return Ok(bookingDTOs);
        }


        [HttpGet("{id}")]
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
                    RoomCount = bd.RoomCount,
                    RoomName = bd.Room.RoomName
                }).ToList()
            };

            return Ok(bookingDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(string id)
        {
            var result = await _bookingService.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Booking not found.");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingStatus(string id, [FromForm] int status)
        {
            var result = await _bookingService.UpdateStatusAsync(id, status);
            if (!result)
            {
                return BadRequest("Unable to update booking status.");
            }

            return NoContent();
        }
    }
}

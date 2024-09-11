using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Data
{
    public class BookingDetail
    {
        public int BookingDetailId { get; set; }
        public string BookingId { get; set; }
        public Guid RoomId { get; set; }
        public long UnitPrice { get; set; }
        public int RoomCount { get; set; }
        public Booking Booking { get; set; }
        public Room Room { get; set; }
    }
}

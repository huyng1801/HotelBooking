using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTO
{
    public class HotelImageDTO
    {
        public int HotelImageId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

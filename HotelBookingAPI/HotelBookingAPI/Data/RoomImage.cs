using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Data
{
    public class RoomImage
    {
        public int RoomImageId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}

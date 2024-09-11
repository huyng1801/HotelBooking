using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Data
{
    public class RoomAmenity
    {
        public int RoomAmenityId { get; set; }
        public Guid RoomId { get; set; }
        public Guid AmenityId { get; set; }
        public Room Room { get; set; }
        public Amenity Amenity { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingAPI.Data
{
    public class Amenity
    {
        public Guid AmenityId { get; set; }
        public string AmenityName { get; set; }
        public virtual ICollection<RoomAmenity> RoomAmenities { get; set; }

    }
}

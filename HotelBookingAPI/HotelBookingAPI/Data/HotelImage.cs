using static System.Net.Mime.MediaTypeNames;

namespace HotelBookingAPI.Data
{
    public class HotelImage
    {
        public int HotelImageId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}

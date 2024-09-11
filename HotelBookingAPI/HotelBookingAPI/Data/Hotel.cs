namespace HotelBookingAPI.Data
{
    public class Hotel
    {
        public Hotel()
        {
            Rooms = new HashSet<Room>();
            HotelImages = new HashSet<HotelImage>();
        }

        public Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string AccommodationPolicy { get; set; }
        public string Description { get; set; }
        public int Star { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<HotelImage> HotelImages { get; set; }
    }
}

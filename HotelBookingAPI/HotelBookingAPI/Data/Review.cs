namespace HotelBookingAPI.Data
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }
    }
}

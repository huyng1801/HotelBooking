namespace HotelBookingAPI.Data
{
    public class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            Reviews = new HashSet<Review>();
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public int Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } 
        public virtual ICollection<Review> Reviews { get; set; }
    }
}

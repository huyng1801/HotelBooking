namespace HotelBookingAPI.Data
{
    public class Booking
    {
        public Booking()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        public string BookingId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public long TotalAmount { get; set; }
        public int Status { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public string? Note { get; set; }
        public string PaymentMethod { get; set; } 
        public bool PaymentStatus { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}

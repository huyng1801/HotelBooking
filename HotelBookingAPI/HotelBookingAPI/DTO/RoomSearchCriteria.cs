namespace HotelBookingAPI.DTO
{
    public class RoomSearchCriteria
    {
        public string RoomName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? IsAvailable { get; set; }
        public Guid? RoomTypeId { get; set; }
        public Guid? HotelId { get; set; }
    }
}

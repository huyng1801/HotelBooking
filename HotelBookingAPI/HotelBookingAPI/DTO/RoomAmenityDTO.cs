using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTO
{
    public class RoomAmenityDTO
    {
        public int RoomAmenityId { get; set; }
        public Guid RoomId { get; set; }
        public string? RoomName { get; set; }
        public Guid AmenityId { get; set; }
        public string? AmenityName { get; set; }
    }
}

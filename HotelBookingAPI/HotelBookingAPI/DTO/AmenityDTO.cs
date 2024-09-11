using HotelBookingAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTO
{
    public class AmenityDTO
    {
        public Guid AmenityId { get; set; }
        public string AmenityName { get; set; }
    }
    public class AmenityRequestDTO
    {
        [Required(ErrorMessage = "Tên tiện nghi là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên tiện nghi không được vượt quá 50 ký tự.")]
        public string AmenityName { get; set; }
    }
}

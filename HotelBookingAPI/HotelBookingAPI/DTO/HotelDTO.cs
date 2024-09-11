using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTO
{
    public class HotelDTO
    {
        public Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string AccommodationPolicy { get; set; }
        public string Description { get; set; }
        public int StarRating { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<HotelImageDTO> Images { get; set; } = new List<HotelImageDTO>();
    }
    public class HotelRequestDTO
    {
        [Required(ErrorMessage = "Tên khách sạn là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên khách sạn không được vượt quá 100 ký tự.")]
        public string HotelName { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Thành phố là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Thành phố không được vượt quá 100 ký tự.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự.")]
        public string Address { get; set; }
        public string AccommodationPolicy { get; set; }
        public string Description { get; set; }

        [Range(1, 5, ErrorMessage = "Số sao phải từ 1 đến 5.")]
        public int Star { get; set; }
        public bool IsActive { get; set; } = true;
        public List<HotelImageDTO>? Images { get; set; } = new List<HotelImageDTO>();
    }
}

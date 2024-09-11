using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTO
{
    public class RoomDTO
    {
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public long Price { get; set; }
        public int RoomCount { get; set; }
        public double Area { get; set; }
        public int NumberPerson { get; set; }
        public string View { get; set; }
        public bool EatBreakfast { get; set; }
        public string BedDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<RoomImageDTO> ImageUrls { get; set; } = new List<RoomImageDTO>();
        public List<RoomAmenityDTO> RoomAmenities { get; set; } = new List<RoomAmenityDTO>();
    }

    public class RoomRequstDTO
    {
        [Required(ErrorMessage = "Tên phòng là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên phòng không được vượt quá 50 ký tự.")]
        public string RoomName { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc.")]
        [Range(0, 100000000, ErrorMessage = "Giá không hợp lệ.")]
        public long Price { get; set; }

        [Required(ErrorMessage = "Số lượng phòng là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phòng không hợp lệ.")]
        public int RoomCount { get; set; }

        [Required(ErrorMessage = "Diện tích là bắt buộc.")]
        [Range(1, double.MaxValue, ErrorMessage = "Diện tích không hợp lệ.")]
        public double Area { get; set; }

        [Required(ErrorMessage = "Số người tối đa là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số người tối đa không hợp lệ.")]
        public int NumberPerson { get; set; }

        [StringLength(100, ErrorMessage = "Hướng phòng không được vượt quá 100 ký tự.")]
        public string View { get; set; }

        public bool EatBreakfast { get; set; }

        [StringLength(100, ErrorMessage = "Mô tả giường không được vượt quá 100 ký tự.")]
        public string BedDescription { get; set; }
        public Guid HotelId { get; set; }
        public List<RoomImageDTO>? ImageUrls { get; set; } = new List<RoomImageDTO>();
    }
}

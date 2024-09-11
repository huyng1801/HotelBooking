using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTO
{
    public class BookingDetailDTO
    {
        public int BookingDetailId { get; set; }
        public string? BookingId { get; set; }

        [Required(ErrorMessage = "Mã phòng là bắt buộc.")]
        public Guid RoomId { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc.")]
        [Range(0, 100000000, ErrorMessage = "Giá không hợp lệ.")]
        public long UnitPrice { get; set; }

        [Required(ErrorMessage = "Số lượng phòng là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phòng ít nhất là 1.")]
        public int RoomCount { get; set; }

        public string? RoomName { get; set; }
    }
}

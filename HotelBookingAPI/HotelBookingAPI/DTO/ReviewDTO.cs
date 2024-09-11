using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTO
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }

        [Required(ErrorMessage = "Nội dung đánh giá là bắt buộc.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Xếp hạng là bắt buộc.")]
        [Range(1, 5, ErrorMessage = "Xếp hạng phải từ 1 đến 5.")]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Mã phòng là bắt buộc.")]
        public Guid RoomId { get; set; }

        [Required(ErrorMessage = "Mã người dùng là bắt buộc.")]
        public Guid UserId { get; set; }

        public string? RoomName { get; set; }
        public string? Username { get; set; }
    }
}

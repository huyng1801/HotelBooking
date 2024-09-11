using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelBookingAPI.Data;

namespace HotelBookingAPI.DTO
{
    public class BookingDTO
    {
        public string BookingId { get; set; }

        [Required(ErrorMessage = "Ngày nhận phòng là bắt buộc.")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Ngày trả phòng là bắt buộc.")]
        public DateTime CheckOutDate { get; set; }

        public long TotalAmount { get; set; }

        public int Status { get; set; }

        [Required(ErrorMessage = "Số người lớn là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng người lớn ít nhất là 1.")]
        public int NumberOfAdults { get; set; }

        [Required(ErrorMessage = "Số trẻ em là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng trẻ em ít nhất là 0.")]
        public int NumberOfChildren { get; set; }

        public string? Note { get; set; }

        [Required(ErrorMessage = "Phương thức thanh toán là bắt buộc.")]
        public string PaymentMethod { get; set; }

        public bool PaymentStatus { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid UserId { get; set; }
        public List<BookingDetailDTO>? BookingDetails { get; set; }
    }
}

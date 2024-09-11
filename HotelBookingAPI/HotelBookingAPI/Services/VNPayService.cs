using HotelBookingAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HotelBookingAPI.Services
{
    public class VNPayService : IVNPayService
    {
        private readonly string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly string vnp_TmnCode = "WZILG6AZ"; // Your TMN code
        private readonly string vnp_HashSecret = "SXTANFDPGUZMDRTIBUQEBUVLRNYLADYW"; // Your hash secret

        public string CreatePaymentUrl(BookingDTO booking, string returnUrl)
        {
            var vnpay = new Dictionary<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", vnp_TmnCode },
                { "vnp_Amount", ((long)(booking.TotalAmount * 100)).ToString() },
                { "vnp_CurrCode", "VND" },
                { "vnp_TxnRef", booking.BookingId.ToString() },
                { "vnp_OrderInfo", $"Thanh toan cho ma don {booking.BookingId}" },
                { "vnp_OrderType", "billpayment" },
                { "vnp_Locale", "vn" },
                { "vnp_ReturnUrl", returnUrl },
                { "vnp_IpAddr", "127.0.0.1" }, // Get IP address dynamically if possible
                { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") }
            };

            var hashData = vnpay.OrderBy(k => k.Key)
                .Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}")
                .ToList();
            var queryString = string.Join("&", hashData);

            var signData = $"{queryString}&vnp_SecureHash={HmacSHA256(vnp_HashSecret, queryString)}";

            return $"{vnp_Url}?{signData}";
        }

        private string HmacSHA256(string key, string inputData)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                var hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputData));
                return string.Concat(hashValue.Select(b => b.ToString("X2")));
            }
        }

        public bool ValidateSignature(string inputData, string vnpSecureHash)
        {
            var hashData = HmacSHA256(vnp_HashSecret, inputData);
            return hashData.Equals(vnpSecureHash, StringComparison.InvariantCultureIgnoreCase);
        }
    }

    public interface IVNPayService
    {
        string CreatePaymentUrl(BookingDTO booking, string returnUrl);
        bool ValidateSignature(string inputData, string vnpSecureHash);
    }
}

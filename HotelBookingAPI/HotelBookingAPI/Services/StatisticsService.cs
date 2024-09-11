using HotelBookingAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelBookingAPI.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext _context;

        public StatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalHotelsAsync()
        {
            return await _context.Hotels.CountAsync();
        }

        public async Task<int> GetTotalRoomsAsync()
        {
            return await _context.Rooms.CountAsync();
        }

        public async Task<int> GetTotalBlogPostsAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetTotalBookingsAsync()
        {
            return await _context.Bookings.CountAsync();
        }

        public async Task<decimal> GetDailyRevenueAsync(DateTime date)
        {
            return await _context.Bookings
                .Where(b => b.CheckInDate.Date == date.Date)
                .SumAsync(b => b.TotalAmount);
        }

        public async Task<decimal> GetMonthlyRevenueAsync(int month, int year)
        {
            return await _context.Bookings
                .Where(b => b.CheckInDate.Month == month && b.CheckInDate.Year == year)
                .SumAsync(b => b.TotalAmount);
        }

        public async Task<decimal> GetYearlyRevenueAsync(int year)
        {
            return await _context.Bookings
                .Where(b => b.CheckInDate.Year == year)
                .SumAsync(b => b.TotalAmount);
        }
    }
    public interface IStatisticsService
    {
        Task<int> GetTotalHotelsAsync();
        Task<int> GetTotalRoomsAsync();
        Task<int> GetTotalBlogPostsAsync();
        Task<int> GetTotalBookingsAsync();
        Task<decimal> GetDailyRevenueAsync(DateTime date);
        Task<decimal> GetMonthlyRevenueAsync(int month, int year);
        Task<decimal> GetYearlyRevenueAsync(int year);
    }
}

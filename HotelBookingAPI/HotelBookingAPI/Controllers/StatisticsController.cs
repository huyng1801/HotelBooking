using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using HotelBookingAPI.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("total/hotels")]
        public async Task<IActionResult> GetTotalHotels()
        {
            var totalHotels = await _statisticsService.GetTotalHotelsAsync();
            return Ok(totalHotels);
        }

        [HttpGet("total/rooms")]
        public async Task<IActionResult> GetTotalRooms()
        {
            var totalRooms = await _statisticsService.GetTotalRoomsAsync();
            return Ok(totalRooms);
        }

        [HttpGet("total/blogPosts")]
        public async Task<IActionResult> GetTotalBlogPosts()
        {
            var totalBlogPosts = await _statisticsService.GetTotalBlogPostsAsync();
            return Ok(totalBlogPosts);
        }

        [HttpGet("total/bookings")]
        public async Task<IActionResult> GetTotalBookings()
        {
            var totalBookings = await _statisticsService.GetTotalBookingsAsync();
            return Ok(totalBookings);
        }

        [HttpGet("revenue/daily")]
        public async Task<IActionResult> GetDailyRevenue([FromQuery] DateTime date)
        {
            var dailyRevenue = await _statisticsService.GetDailyRevenueAsync(date);
            return Ok(dailyRevenue);
        }

        [HttpGet("revenue/monthly")]
        public async Task<IActionResult> GetMonthlyRevenue([FromQuery] int month, [FromQuery] int year)
        {
            var monthlyRevenue = await _statisticsService.GetMonthlyRevenueAsync(month, year);
            return Ok(monthlyRevenue);
        }

        [HttpGet("revenue/yearly")]
        public async Task<IActionResult> GetYearlyRevenue([FromQuery] int year)
        {
            var yearlyRevenue = await _statisticsService.GetYearlyRevenueAsync(year);
            return Ok(yearlyRevenue);
        }
    }

}

using HotelBookingAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingAPI.Services
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly ApplicationDbContext _context;

        public BookingDetailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookingDetail>> GetAllAsync()
        {
            return await _context.BookingDetails.Include(bd => bd.Room).ToListAsync();
        }

        public async Task<BookingDetail> GetByIdAsync(int bookingDetailId)
        {
            return await _context.BookingDetails.Include(bd => bd.Room).FirstOrDefaultAsync(bd => bd.BookingDetailId == bookingDetailId);
        }

        public async Task<IEnumerable<BookingDetail>> GetByBookingIdAsync(string bookingId)
        {
            return await _context.BookingDetails.Include(bd => bd.Room).Where(bd => bd.BookingId == bookingId).ToListAsync();
        }

        public async Task<BookingDetail> CreateAsync(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Add(bookingDetail);
            await _context.SaveChangesAsync();
            return bookingDetail;
        }

        public async Task<bool> UpdateAsync(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Update(bookingDetail);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int bookingDetailId)
        {
            var bookingDetail = await _context.BookingDetails.FindAsync(bookingDetailId);
            if (bookingDetail == null)
            {
                return false;
            }
            _context.BookingDetails.Remove(bookingDetail);
            return await _context.SaveChangesAsync() > 0;
        }
    }

    public interface IBookingDetailService
    {
        Task<IEnumerable<BookingDetail>> GetAllAsync();
        Task<BookingDetail> GetByIdAsync(int bookingDetailId);
        Task<IEnumerable<BookingDetail>> GetByBookingIdAsync(string bookingId);
        Task<BookingDetail> CreateAsync(BookingDetail bookingDetail);
        Task<bool> UpdateAsync(BookingDetail bookingDetail);
        Task<bool> DeleteAsync(int bookingDetailId);
    }
}

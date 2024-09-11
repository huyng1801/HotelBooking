using HotelBookingAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews.Include(r => r.Room).Include(r => r.User).ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int reviewId)
        {
            return await _context.Reviews.Include(r => r.Room).Include(r => r.User).FirstOrDefaultAsync(r => r.ReviewId == reviewId);
        }

        public async Task<Review> CreateAsync(Review review)
        {
            review.CreatedAt = DateTime.UtcNow;
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> UpdateAsync(Review review)
        {
            var existingReview = await _context.Reviews.FindAsync(review.ReviewId);
            if (existingReview == null)
            {
                return false;
            }

            existingReview.Content = review.Content;
            existingReview.Rating = review.Rating;
            existingReview.RoomId = review.RoomId;
            existingReview.UserId = review.UserId;

            _context.Reviews.Update(existingReview);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            _context.Reviews.Remove(review);
            return await _context.SaveChangesAsync() > 0;
        }
    }

    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int reviewId);
        Task<Review> CreateAsync(Review review);
        Task<bool> UpdateAsync(Review review);
        Task<bool> DeleteAsync(int reviewId);
    }
}

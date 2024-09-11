using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingAPI.Data;

namespace HotelBookingAPI.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly ApplicationDbContext _context;

        public AmenityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Amenity>> GetAllAsync()
        {
            return await _context.Amenities.ToListAsync();
        }

        public async Task<Amenity> GetByIDAsync(Guid amenityID)
        {
            return await _context.Amenities.FirstOrDefaultAsync(a => a.AmenityId == amenityID);
        }

        public async Task<bool> InsertAsync(Amenity amenity)
        {
            if (amenity == null)
            {
                throw new ArgumentNullException(nameof(amenity));
            }

            try
            {
                _context.Amenities.Add(amenity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Amenity amenity)
        {
            if (amenity == null)
            {
                throw new ArgumentNullException(nameof(amenity));
            }

            try
            {
                var existingAmenity = await _context.Amenities.FindAsync(amenity.AmenityId);
                if (existingAmenity == null)
                {
                    return false;
                }

                existingAmenity.AmenityName = amenity.AmenityName;
                _context.Amenities.Update(existingAmenity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid amenityID)
        {
            var amenity = await _context.Amenities.FindAsync(amenityID);
            if (amenity == null)
            {
                return false;
            }

            try
            {
                _context.Amenities.Remove(amenity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CheckAmenityNameExistsAsync(string amenityName)
        {
            return await _context.Amenities.AnyAsync(a => a.AmenityName == amenityName);
        }
    }

    public interface IAmenityService
    {
        Task<IEnumerable<Amenity>> GetAllAsync();
        Task<Amenity> GetByIDAsync(Guid amenityID);
        Task<bool> InsertAsync(Amenity amenity);
        Task<bool> UpdateAsync(Amenity amenity);
        Task<bool> DeleteAsync(Guid amenityID);
        Task<bool> CheckAmenityNameExistsAsync(string amenityName);
    }
}

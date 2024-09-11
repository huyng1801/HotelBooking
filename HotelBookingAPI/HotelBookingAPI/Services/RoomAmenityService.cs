using HotelBookingAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingAPI.Services
{
    public class RoomAmenityService : IRoomAmenityService
    {
        private readonly ApplicationDbContext _context;

        public RoomAmenityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomAmenity>> GetAllAsync()
        {
            return await _context.RoomAmenities.Include(ra => ra.Room).Include(ra => ra.Amenity).ToListAsync();
        }

        public async Task<RoomAmenity> GetByIdAsync(int id)
        {
            return await _context.RoomAmenities.Include(ra => ra.Room).Include(ra => ra.Amenity).FirstOrDefaultAsync(ra => ra.RoomAmenityId == id);
        }
        public async Task<IEnumerable<RoomAmenity>> GetByRoomIdAsync(Guid roomId)
        {
            return await _context.RoomAmenities
                .Include(ra => ra.Amenity)
                .Include(ra => ra.Room)
                .Where(ra => ra.RoomId == roomId)
                .ToListAsync();
        }

        public async Task<bool> InsertAsync(RoomAmenity roomAmenity)
        {
            if (roomAmenity == null)
            {
                throw new ArgumentNullException(nameof(roomAmenity));
            }

            try
            {
                _context.RoomAmenities.Add(roomAmenity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(RoomAmenity roomAmenity)
        {
            if (roomAmenity == null)
            {
                throw new ArgumentNullException(nameof(roomAmenity));
            }

            try
            {
                var existingRoomAmenity = await _context.RoomAmenities.FindAsync(roomAmenity.RoomAmenityId);
                if (existingRoomAmenity == null)
                {
                    return false;
                }

                existingRoomAmenity.RoomId = roomAmenity.RoomId;
                existingRoomAmenity.AmenityId = roomAmenity.AmenityId;

                _context.RoomAmenities.Update(existingRoomAmenity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var roomAmenity = await _context.RoomAmenities.FindAsync(id);
            if (roomAmenity == null)
            {
                return false;
            }

            try
            {
                _context.RoomAmenities.Remove(roomAmenity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public interface IRoomAmenityService
    {
        Task<IEnumerable<RoomAmenity>> GetAllAsync();
        Task<RoomAmenity> GetByIdAsync(int id);
        Task<IEnumerable<RoomAmenity>> GetByRoomIdAsync(Guid roomId);
        Task<bool> InsertAsync(RoomAmenity roomAmenity);
        Task<bool> UpdateAsync(RoomAmenity roomAmenity);
        Task<bool> DeleteAsync(int id);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingAPI.Data;
using HotelBookingAPI.DTO;

namespace HotelBookingAPI.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext _context;
        private const string ImageFolderPath = "Images/Hotels";

        public HotelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels.Include(h => h.HotelImages).ToListAsync();
        }

        public async Task<Hotel> GetByIDAsync(Guid hotelId)
        {
            return await _context.Hotels.Include(h => h.HotelImages).FirstOrDefaultAsync(h => h.HotelId == hotelId);
        }

        public async Task<bool> InsertAsync(Hotel hotel, IFormFileCollection imageFiles)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            try
            {
                if (imageFiles != null && imageFiles.Count > 0)
                {
                    hotel.HotelImages = new List<HotelImage>();
                    foreach (var imageFile in imageFiles)
                    {
                        var imageUrl = await SaveImage(imageFile);
                        hotel.HotelImages.Add(new HotelImage { ImageUrl = imageUrl, CreatedAt = DateTime.UtcNow });
                    }
                }

                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Hotel hotel, IFormFileCollection imageFiles)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            try
            {
                var existingHotel = await _context.Hotels.Include(h => h.HotelImages).FirstOrDefaultAsync(h => h.HotelId == hotel.HotelId);
                if (existingHotel == null)
                {
                    return false;
                }

                existingHotel.HotelName = hotel.HotelName;
                existingHotel.City = hotel.City;
                existingHotel.PhoneNumber = hotel.PhoneNumber;
                existingHotel.Address = hotel.Address;
                existingHotel.AccommodationPolicy = hotel.AccommodationPolicy;
                existingHotel.Description = hotel.Description;
                existingHotel.Star = hotel.Star;
                existingHotel.IsActive = hotel.IsActive;
                existingHotel.UpdatedAt = DateTime.UtcNow;

                // Remove all existing images
                foreach (var image in existingHotel.HotelImages)
                {
                    DeleteImage(image.ImageUrl);
                    _context.HotelImages.Remove(image);
                }
                existingHotel.HotelImages.Clear();

                // Add new images
                if (imageFiles != null && imageFiles.Count > 0)
                {
                    foreach (var imageFile in imageFiles)
                    {
                        var imageUrl = await SaveImage(imageFile);
                        existingHotel.HotelImages.Add(new HotelImage { ImageUrl = imageUrl, CreatedAt = DateTime.UtcNow });
                    }
                }

                _context.Hotels.Update(existingHotel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid hotelId)
        {
            var hotel = await _context.Hotels.Include(h => h.HotelImages).FirstOrDefaultAsync(h => h.HotelId == hotelId);
            if (hotel == null)
            {
                return false;
            }

            try
            {
                foreach (var hotelImage in hotel.HotelImages)
                {
                    DeleteImage(hotelImage.ImageUrl);
                }

                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(ImageFolderPath, imageName);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(ImageFolderPath, imageName);

            // Ensure the directory exists
            var directory = Path.GetDirectoryName(imagePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return imageName;
        }

        public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(Guid hotelId)
        {
            return await _context.Rooms.Include(r => r.RoomImages).Include(r => r.RoomAmenities).ThenInclude(ra => ra.Amenity).Where(r => r.HotelId == hotelId).ToListAsync();
        }
        public async Task<IEnumerable<Hotel>> SearchHotelsAsync(string city, string address, DateTime checkInDate, DateTime checkOutDate, int numberOfPersons, int numberOfRooms)
        {
            var hotels = _context.Hotels.AsQueryable();

            if (!string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(address))
            {
                hotels = hotels.Where(h => (!string.IsNullOrEmpty(city) && h.City == city) ||
                                           (!string.IsNullOrEmpty(address) && h.Address == address));
            }

            var availableHotels = new List<Hotel>();

            foreach (var hotel in await hotels.Include(h => h.HotelImages)
                                              .Include(h => h.Rooms)
                                              .ThenInclude(r => r.BookingDetails)
                                              .ToListAsync())
            {
                var totalRooms = hotel.Rooms.Sum(r => r.RoomCount);
                var totalCapacity = hotel.Rooms.Sum(r => r.RoomCount * r.NumberPerson);

                bool isRoomAvailable = true;

                foreach (var room in hotel.Rooms)
                {
                    var bookedRoomCount = room.BookingDetails
                        .Where(bd => bd.Booking.CheckInDate < checkOutDate && bd.Booking.CheckOutDate > checkInDate)
                        .Sum(bd => bd.RoomCount);

                    if (bookedRoomCount + numberOfRooms > room.RoomCount)
                    {
                        isRoomAvailable = false;
                        break;
                    }
                }

                if (isRoomAvailable && totalRooms >= numberOfRooms && totalCapacity >= numberOfPersons)
                {
                    availableHotels.Add(hotel);
                }
            }

            return availableHotels;
        }


    }

    public interface IHotelService
    {
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<Hotel> GetByIDAsync(Guid hotelId);
        Task<bool> InsertAsync(Hotel hotel, IFormFileCollection imageFiles);
        Task<bool> UpdateAsync(Hotel hotel, IFormFileCollection imageFiles);
        Task<bool> DeleteAsync(Guid hotelId);
        Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(Guid hotelId);
        Task<IEnumerable<Hotel>> SearchHotelsAsync(string city, string address, DateTime checkInDate, DateTime checkOutDate, int numberOfPersons, int numberOfRooms);
    }
}

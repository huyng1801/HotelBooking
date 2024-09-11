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
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _context;
        private const string ImageFolderPath = "Images/Rooms";

        public RoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms
                .Include(r => r.RoomImages)
                .ToListAsync();
        }

        public async Task<Room> GetByIdAsync(Guid roomId)
        {
            return await _context.Rooms
                .Include(r => r.RoomImages)
                .FirstOrDefaultAsync(r => r.RoomId == roomId);
        }

        public async Task<bool> InsertAsync(Room room, IFormFileCollection imageFiles)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            try
            {
                if (imageFiles != null && imageFiles.Count > 0)
                {
                    room.RoomImages = new List<RoomImage>();
                    foreach (var imageFile in imageFiles)
                    {
                        var imageUrl = await SaveImage(imageFile);
                        room.RoomImages.Add(new RoomImage
                        {
                            ImageUrl = imageUrl,
                            CreatedAt = DateTime.Now
                        });
                    }
                }

                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Room room, IFormFileCollection imageFiles)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            try
            {
                var existingRoom = await _context.Rooms
                    .Include(r => r.RoomImages)
                    .FirstOrDefaultAsync(r => r.RoomId == room.RoomId);
                if (existingRoom == null)
                {
                    return false;
                }

                existingRoom.RoomName = room.RoomName;
                existingRoom.Price = room.Price;
                existingRoom.RoomCount = room.RoomCount;
                existingRoom.Area = room.Area;
                existingRoom.NumberPerson = room.NumberPerson;
                existingRoom.View = room.View;
                existingRoom.EatBreakfast = room.EatBreakfast;
                existingRoom.BedDescription = room.BedDescription;
                existingRoom.HotelId = room.HotelId;
                existingRoom.UpdatedAt = DateTime.UtcNow;

                // Remove all existing images
                foreach (var image in existingRoom.RoomImages)
                {
                    DeleteImage(image.ImageUrl);
                }
                existingRoom.RoomImages.Clear();

                // Add new images
                if (imageFiles != null && imageFiles.Count > 0)
                {
                    foreach (var imageFile in imageFiles)
                    {
                        var imageUrl = await SaveImage(imageFile);
                        existingRoom.RoomImages.Add(new RoomImage
                        {
                            ImageUrl = imageUrl,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }

                _context.Rooms.Update(existingRoom);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid roomId)
        {
            var room = await _context.Rooms
                .Include(r => r.RoomImages)
                .FirstOrDefaultAsync(r => r.RoomId == roomId);
            if (room == null)
            {
                return false;
            }

            try
            {
                foreach (var roomImage in room.RoomImages)
                {
                    DeleteImage(roomImage.ImageUrl);
                }

                _context.Rooms.Remove(room);
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
            if (!Directory.Exists(ImageFolderPath))
            {
                Directory.CreateDirectory(ImageFolderPath);
            }

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return imageName;
        }

    }

    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(Guid roomId);
        Task<bool> InsertAsync(Room room, IFormFileCollection imageFiles);
        Task<bool> UpdateAsync(Room room, IFormFileCollection imageFiles);
        Task<bool> DeleteAsync(Guid roomId);
    }
}

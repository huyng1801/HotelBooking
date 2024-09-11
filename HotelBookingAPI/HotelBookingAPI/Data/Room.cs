using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Data
{
    public class Room
    {
        public Room()
        {
            RoomAmenities = new HashSet<RoomAmenity>();
            BookingDetails = new HashSet<BookingDetail>();
            RoomImages = new HashSet<RoomImage>();
            Reviews = new HashSet<Review>();
        }

        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public long Price { get; set; }
        public int RoomCount { get; set; }
        public double Area { get; set; }
        public int NumberPerson { get; set; }
        public string View { get; set; }
        public bool EatBreakfast { get; set; }
        public string BedDescription { get; set; } 
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<RoomAmenity> RoomAmenities { get; set; }
        public ICollection<BookingDetail> BookingDetails { get; set; }
        public ICollection<RoomImage> RoomImages { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}

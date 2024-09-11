using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Hotel> Hotels { get; set; } 
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.HotelId).HasName("PK_HotelId");
                entity.Property(e => e.HotelName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.AccommodationPolicy) 
                    .HasColumnType("NTEXT");
                entity.Property(e => e.Description)
                    .HasColumnType("NTEXT");
                entity.Property(e => e.Star)
                    .IsRequired();
                entity.Property(e => e.IsActive)
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<HotelImage>(entity =>
            {
                entity.HasKey(e => e.HotelImageId).HasName("PK_HotelImageId");
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(200);
                entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.HotelId).IsRequired();
                entity.HasOne(hi => hi.Hotel)
                    .WithMany(h => h.HotelImages)
                    .HasForeignKey(hi => hi.HotelId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_HotelImage_Hotel");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                  .HasName("PK_RoomId");
                entity.Property(r => r.RoomName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(r => r.Price)
                    .IsRequired();
                entity.Property(r => r.RoomCount)
                    .IsRequired();
                entity.Property(r => r.Area)
                    .IsRequired();
                entity.Property(r => r.NumberPerson)
                    .IsRequired();
                entity.Property(r => r.View)
                    .HasMaxLength(100);
                entity.Property(r => r.EatBreakfast)
                    .IsRequired(); 
                entity.Property(r => r.BedDescription)
                    .HasMaxLength(100);
                entity.Property(e => e.HotelId).IsRequired();
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()")
                    .ValueGeneratedOnAddOrUpdate();
                entity.HasOne(e => e.Hotel)
                    .WithMany(h => h.Rooms)
                    .HasForeignKey(e => e.HotelId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RoomImage>(entity =>
            {
                entity.HasKey(e => e.RoomImageId);
                entity.Property(e => e.ImageUrl).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.RoomId).IsRequired();
                entity.HasOne(e => e.Room)
                      .WithMany(r => r.RoomImages)
                      .HasForeignKey(e => e.RoomId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Amenity>(entity =>
            {
                entity.HasKey(e => e.AmenityId)
                    .HasName("PK_AmenityId");
                entity.Property(e => e.AmenityName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RoomAmenity>(entity =>
            {
                entity.HasKey(e => e.RoomAmenityId)
                    .HasName("PK_RoomAmenityId");
                entity.Property(e => e.RoomId).IsRequired();
                entity.Property(e => e.AmenityId).IsRequired();
                entity.HasOne(ra => ra.Room)
                    .WithMany(r => r.RoomAmenities)
                    .HasForeignKey(ra => ra.RoomId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RoomAmenity_Room");

                entity.HasOne(ra => ra.Amenity)
                    .WithMany(a => a.RoomAmenities)
                    .HasForeignKey(ra => ra.AmenityId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RoomAmenity_Amenity");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).IsRequired();
                entity.Property(e => e.Username).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK_UserId");
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.HashPassword)
                    .IsRequired();
                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(320);
                entity.Property(e => e.BirthDate)
                    .IsRequired()
                    .HasColumnType("DATE");
                entity.Property(e => e.Country)
                    .HasMaxLength(100);
                entity.Property(e => e.Role)
                    .IsRequired();
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValue(true);
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasData(new User
                {
                    UserId = Guid.NewGuid(),
                    Username = "admin",
                    HashPassword = "E10ADC3949BA59ABBE56E057F20F883E",
                    FullName = "Người quản lý",
                    PhoneNumber = "0123456789",
                    Email = "admin@example.com",
                    BirthDate = new DateTime(1980, 1, 1),
                    Country = "Vietnam",
                    Role = 0,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("PK_BookingId");
                entity.Property(e => e.BookingId)
                    .HasMaxLength(50) 
                    .IsRequired();
                entity.Property(e => e.CheckInDate)
                    .IsRequired();
                entity.Property(e => e.CheckOutDate)
                    .IsRequired();
                entity.Property(e => e.TotalAmount)
                    .IsRequired();
                entity.Property(e => e.Status)
                    .IsRequired();
                entity.Property(e => e.NumberOfAdults)
                    .IsRequired();
                entity.Property(e => e.NumberOfChildren)
                    .IsRequired();
                entity.Property(e => e.Note)
                    .HasMaxLength(500);
                entity.Property(e => e.PaymentMethod)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.PaymentStatus)
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()")
                    .ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.UserId)
                    .IsRequired();
                entity.HasOne(e => e.User)
                    .WithMany(c => c.Bookings)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Booking_User");
            });

            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.HasKey(e => e.BookingDetailId)
                    .HasName("PK_BookingDetailId");
                entity.Property(bd => bd.BookingId)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(bd => bd.RoomId)
                    .IsRequired();
                entity.Property(bd => bd.RoomCount)
                    .IsRequired();
                entity.Property(bd => bd.UnitPrice)
                    .IsRequired();
                entity.HasOne(bd => bd.Booking)
                    .WithMany(b => b.BookingDetails)
                    .HasForeignKey(bd => bd.BookingId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_BookingDetail_Booking");
                entity.HasOne(bd => bd.Room)
                    .WithMany(r => r.BookingDetails)
                    .HasForeignKey(bd => bd.RoomId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_BookingDetail_Room");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK_ReviewId");
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("NTEXT");
                entity.Property(e => e.Rating)
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.RoomId)
                    .IsRequired();
                entity.Property(e => e.UserId)
                    .IsRequired();
                entity.HasOne(r => r.Room)
                    .WithMany(r => r.Reviews)
                    .HasForeignKey(r => r.RoomId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Review_Room");
                entity.HasOne(r => r.User)
                    .WithMany(c => c.Reviews)
                    .HasForeignKey(r => r.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Review_User");
            });
        }
    }
}

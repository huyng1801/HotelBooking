USE [master]
GO
/****** Object:  Database [HotelBooking]    Script Date: 6/26/2024 10:01:58 PM ******/
CREATE DATABASE [HotelBooking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HotelBooking', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HotelBooking.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HotelBooking_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HotelBooking_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HotelBooking] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HotelBooking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HotelBooking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HotelBooking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HotelBooking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HotelBooking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HotelBooking] SET ARITHABORT OFF 
GO
ALTER DATABASE [HotelBooking] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HotelBooking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HotelBooking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HotelBooking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HotelBooking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HotelBooking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HotelBooking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HotelBooking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HotelBooking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HotelBooking] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HotelBooking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HotelBooking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HotelBooking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HotelBooking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HotelBooking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HotelBooking] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [HotelBooking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HotelBooking] SET RECOVERY FULL 
GO
ALTER DATABASE [HotelBooking] SET  MULTI_USER 
GO
ALTER DATABASE [HotelBooking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HotelBooking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HotelBooking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HotelBooking] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HotelBooking] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HotelBooking] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HotelBooking', N'ON'
GO
ALTER DATABASE [HotelBooking] SET QUERY_STORE = ON
GO
ALTER DATABASE [HotelBooking] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HotelBooking]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/26/2024 10:01:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Amenities]    Script Date: 6/26/2024 10:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Amenities](
	[AmenityId] [uniqueidentifier] NOT NULL,
	[AmenityName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AmenityId] PRIMARY KEY CLUSTERED 
(
	[AmenityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingDetails]    Script Date: 6/26/2024 10:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingDetails](
	[BookingDetailId] [int] IDENTITY(1,1) NOT NULL,
	[BookingId] [nvarchar](50) NOT NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[UnitPrice] [bigint] NOT NULL,
	[RoomCount] [int] NOT NULL,
 CONSTRAINT [PK_BookingDetailId] PRIMARY KEY CLUSTERED 
(
	[BookingDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 6/26/2024 10:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[BookingId] [nvarchar](50) NOT NULL,
	[CheckInDate] [datetime2](7) NOT NULL,
	[CheckOutDate] [datetime2](7) NOT NULL,
	[TotalAmount] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[NumberOfAdults] [int] NOT NULL,
	[NumberOfChildren] [int] NOT NULL,
	[Note] [nvarchar](500) NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[PaymentStatus] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BookingId] PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HotelImages]    Script Date: 6/26/2024 10:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HotelImages](
	[HotelImageId] [int] IDENTITY(1,1) NOT NULL,
	[ImageUrl] [nvarchar](200) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[HotelId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_HotelImageId] PRIMARY KEY CLUSTERED 
(
	[HotelImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotels]    Script Date: 6/26/2024 10:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotels](
	[HotelId] [uniqueidentifier] NOT NULL,
	[HotelName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[AccommodationPolicy] [ntext] NOT NULL,
	[Description] [ntext] NOT NULL,
	[Star] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_HotelId] PRIMARY KEY CLUSTERED 
(
	[HotelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 6/26/2024 10:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Expires] [datetime2](7) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Revoked] [datetime2](7) NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 6/26/2024 10:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[ReviewId] [int] IDENTITY(1,1) NOT NULL,
	[Content] [ntext] NOT NULL,
	[Rating] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ReviewId] PRIMARY KEY CLUSTERED 
(
	[ReviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomAmenities]    Script Date: 6/26/2024 10:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomAmenities](
	[RoomAmenityId] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[AmenityId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RoomAmenityId] PRIMARY KEY CLUSTERED 
(
	[RoomAmenityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomImages]    Script Date: 6/26/2024 10:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomImages](
	[RoomImageId] [int] IDENTITY(1,1) NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RoomImages] PRIMARY KEY CLUSTERED 
(
	[RoomImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 6/26/2024 10:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomId] [uniqueidentifier] NOT NULL,
	[RoomName] [nvarchar](50) NOT NULL,
	[Price] [bigint] NOT NULL,
	[RoomCount] [int] NOT NULL,
	[Area] [float] NOT NULL,
	[NumberPerson] [int] NOT NULL,
	[View] [nvarchar](100) NOT NULL,
	[EatBreakfast] [bit] NOT NULL,
	[BedDescription] [nvarchar](100) NOT NULL,
	[HotelId] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_RoomId] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/26/2024 10:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[HashPassword] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[Role] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UserId] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240625134822_First', N'8.0.0')
GO
INSERT [dbo].[Amenities] ([AmenityId], [AmenityName]) VALUES (N'9fd4c6bc-7407-4239-9bd4-6245727bcf98', N'Dụng cụ vệ sinh cá nhân')
INSERT [dbo].[Amenities] ([AmenityId], [AmenityName]) VALUES (N'ad5ad1ff-086a-48d1-a61c-624fca0769a3', N'Bồn tắm')
INSERT [dbo].[Amenities] ([AmenityId], [AmenityName]) VALUES (N'e19a4265-bd79-44fc-998e-7036a8f775b9', N'Không hút thuốc')
INSERT [dbo].[Amenities] ([AmenityId], [AmenityName]) VALUES (N'2516223d-7606-4d72-baed-8301325d65d4', N'Vòi sen')
INSERT [dbo].[Amenities] ([AmenityId], [AmenityName]) VALUES (N'347e5d51-6625-454e-97f8-8dcb0ddb143a', N'Máy sấy tóc')
INSERT [dbo].[Amenities] ([AmenityId], [AmenityName]) VALUES (N'019c55e6-9b60-43c5-be26-9b204c633a72', N'TV')
INSERT [dbo].[Amenities] ([AmenityId], [AmenityName]) VALUES (N'2bfd62c0-1ce2-4c75-ada7-beb59c6fb92a', N'Điều hòa')
INSERT [dbo].[Amenities] ([AmenityId], [AmenityName]) VALUES (N'cfca7957-e59e-4b9d-91ea-df336c2b7d71', N'Ban công')
GO
SET IDENTITY_INSERT [dbo].[HotelImages] ON 

INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (143, N'009c47f6-32a8-4c78-9c2b-5c9418fc59df.jpg', CAST(N'2024-06-26T01:18:54.7653059' AS DateTime2), N'99503832-3b04-41e5-a428-08dc951e787a')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (144, N'36d979bd-af99-4a44-93d1-a1507854df76.jpg', CAST(N'2024-06-26T01:18:54.7662021' AS DateTime2), N'99503832-3b04-41e5-a428-08dc951e787a')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (145, N'50dbb55c-b7c2-43a5-8820-5e26a02ed025.jpg', CAST(N'2024-06-26T01:18:54.7676478' AS DateTime2), N'99503832-3b04-41e5-a428-08dc951e787a')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (146, N'64b4621a-c2c0-4beb-90f3-941479f4b231.jpg', CAST(N'2024-06-26T01:18:54.7687068' AS DateTime2), N'99503832-3b04-41e5-a428-08dc951e787a')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (147, N'af99684e-c177-40ad-a9f6-2d6944d8cb9d.jpg', CAST(N'2024-06-26T01:18:54.7698822' AS DateTime2), N'99503832-3b04-41e5-a428-08dc951e787a')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (148, N'14485bbf-fd3a-49fe-9972-8e6d9d2fe2b9.jpg', CAST(N'2024-06-26T01:18:54.7716248' AS DateTime2), N'99503832-3b04-41e5-a428-08dc951e787a')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (149, N'ad1ee35e-8ae0-47f0-853f-820820e2e5e9.jpg', CAST(N'2024-06-26T01:18:54.7727949' AS DateTime2), N'99503832-3b04-41e5-a428-08dc951e787a')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (150, N'2e9dcaad-78d9-4bcb-8dd6-3403bad63466.jpg', CAST(N'2024-06-26T01:18:54.7740172' AS DateTime2), N'99503832-3b04-41e5-a428-08dc951e787a')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (151, N'cfb573f6-7f21-418a-98fb-ebcdc7f9a832.jpg', CAST(N'2024-06-26T05:19:32.9260510' AS DateTime2), N'ae851a0b-3c47-4856-0dda-08dc959f9017')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (152, N'95897850-884a-43b5-bd7d-8968b3646172.jpg', CAST(N'2024-06-26T05:19:32.9275927' AS DateTime2), N'ae851a0b-3c47-4856-0dda-08dc959f9017')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (153, N'9a49855f-a5c1-4a4b-9626-25a34858793f.jpg', CAST(N'2024-06-26T05:19:32.9288325' AS DateTime2), N'ae851a0b-3c47-4856-0dda-08dc959f9017')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (154, N'34ea0243-4a5a-4919-9d80-09052afae123.jpg', CAST(N'2024-06-26T05:19:32.9301144' AS DateTime2), N'ae851a0b-3c47-4856-0dda-08dc959f9017')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (155, N'73fbd379-e067-4cee-8cbf-b3669dabbeb9.jpg', CAST(N'2024-06-26T05:19:32.9359917' AS DateTime2), N'ae851a0b-3c47-4856-0dda-08dc959f9017')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (156, N'd8992b31-cb95-4b14-b148-54edb011f789.jpg', CAST(N'2024-06-26T05:19:32.9394394' AS DateTime2), N'ae851a0b-3c47-4856-0dda-08dc959f9017')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (157, N'79190184-a4fb-4462-b731-8e9bf900e654.jpg', CAST(N'2024-06-26T05:19:32.9443350' AS DateTime2), N'ae851a0b-3c47-4856-0dda-08dc959f9017')
INSERT [dbo].[HotelImages] ([HotelImageId], [ImageUrl], [CreatedAt], [HotelId]) VALUES (158, N'0699c86d-9b85-4199-ba96-5c74f4a8fbc1.jpg', CAST(N'2024-06-26T05:19:32.9472050' AS DateTime2), N'ae851a0b-3c47-4856-0dda-08dc959f9017')
SET IDENTITY_INSERT [dbo].[HotelImages] OFF
GO
INSERT [dbo].[Hotels] ([HotelId], [HotelName], [PhoneNumber], [City], [Address], [AccommodationPolicy], [Description], [Star], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'99503832-3b04-41e5-a428-08dc951e787a', N'Khách sạn Delica Hà Nội(Hanoi Delica Hotel)', N'0123456789', N'Hà Nội', N'41 Hàng Quạt, Hàng Gai, Hoàn Kiếm', N'<p>Lưu ý: Chính sách nhận phòng khác nhau tùy theo chỗ lưu trú. Vui lòng kiểm tra cẩn thận trước khi đặt phòng Nơi lưu trú này có dịch vụ đón khách từ bến tàu du lịch và sân bay (có thể tính phụ phí). Để sử dụng dịch vụ, khách phải liên hệ nơi lưu trú qua thông tin liên hệ được cung cấp trong xác nhận đặt phòng 24 giờ trước khi đến. Nhân viên tiếp tân sẽ đón tiếp khách tại nơi lưu trú. Khách chịu trách nhiệm nhận phòng phải từ 18 tuổi trở lên Có thể thu phí thêm người với mức phí khác nhau, tùy chính sách riêng Có thể cần giấy tờ tùy thân hợp pháp có ảnh và cần đặt cọc bằng thẻ tín dụng, thẻ ghi nợ hoặc tiền mặt (cho các chi phí phát sinh - nếu có) khi làm thủ tục nhận phòng Tùy thuộc vào tình hình thực tế khi nhận phòng mà các yêu cầu đặc biệt có được đáp ứng hay không và có thể thu phụ phí. Không đảm bảo đáp ứng mọi yêu cầu đặc biệt Nơi lưu trú này nhận thanh toán bằng thẻ tín dụng, thẻ ghi nợ và tiền mặt</p>', N'<p>Khách sạn Delica Hà Nội ở Phố Cổ, Hà Nội, cách Chợ Đêm Phố Cổ Hà Nội 4 phút đi bộ và Hồ Hoàn Kiếm 6 phút đi bộ. Khách sạn này cách Nhà thờ Lớn Hà Nội 0,3 mi (0,5 km) và cách Nhà hát Múa rối Nước Thăng Long 0,3 mi (0,5 km).</p>', 4, 1, CAST(N'2024-06-25T14:00:17.6053049' AS DateTime2), CAST(N'2024-06-25T21:00:17.6266667' AS DateTime2))
INSERT [dbo].[Hotels] ([HotelId], [HotelName], [PhoneNumber], [City], [Address], [AccommodationPolicy], [Description], [Star], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'ae851a0b-3c47-4856-0dda-08dc959f9017', N'Khách Sạn Old Quarter 1961(Old Quarter Hotel 1961)', N'0123456789', N'Hà Nội', N'6/22 Hang Voi Street- Ly Thai To- Hoan Kiem- Ha Noi', N'<p>Chính sách nhận phòng</p><p><img src="https://res.klook.com/image/upload/v1639722469/UED%20Team%EF%BC%88for%20DE%20only%EF%BC%89/System%20Icon/Hotel/Public%20icon/icon_time_time_s.png" height="16" width="16">Giờ nhận phòng&nbsp;14:00 ~ 14:00</p><p><img src="https://res.klook.com/image/upload/v1639722469/UED%20Team%EF%BC%88for%20DE%20only%EF%BC%89/System%20Icon/Hotel/Public%20icon/icon_time_time_s.png" height="16" width="16">Giờ trả phòng&nbsp;12:00</p><p>Lưu ý: Chính sách nhận phòng khác nhau tùy theo chỗ lưu trú. Vui lòng kiểm tra cẩn thận trước khi đặt phòng</p><p>Khách chịu trách nhiệm nhận phòng phải từ 0 tuổi trở lên</p><p>Chính sách thú cưng</p><p>Chào đón vật nuôi</p>', N'<p><span style="color: rgb(33, 33, 33);">Tọa lạc tại Quận Hoàn Kiếm, Hà Nội, Old Quarter Hotel 1961 cách Nhà hát Múa rối Nước Thăng Long 5 phút đi bộ và cách Hồ Hoàn Kiếm 6 phút đi bộ. Khách sạn cách Ô Quan Chưởng 0,6 mi (1 km) và cách Nhà hát Lớn Hà Nội 0,8 mi (1,3 km). Hãy tận hưởng những dịch vụ, tiện ích như truy cập Internet không dây miễn phí, TV ở khu vực chung và dịch vụ hỗ trợ tour/vé du lịch. Có nhiều dịch vụ, tiện ích dành cho khách, bao gồm báo miễn phí ở sảnh, dịch vụ giặt ủi/giặt khô và quầy tiếp tân phục vụ 24 giờ/ngày. Có chỉ có chỗ đậu xe máy trong khuôn viên. Hãy nghỉ ngơi thoải mái như đang ở nhà mình tại một trong 10 phòng được trang bị máy điều hòa và TV LCD. Truy cập Internet không dây miễn phí giúp quý vị luôn giữ liên lạc; trong khi các chương trình truyền hình cáp sẽ mang đến những phút giây giải trí thoải mái. Các phòng tắm được trang bị buồng tắm vòi sen với vòi sen phun mưa và đồ dùng nhà tắm miễn phí. Phòng được trang bị điện thoại; và dịch vụ dọn phòng phục vụ hàng ngày.</span></p>', 3, 1, CAST(N'2024-06-26T05:19:32.9153469' AS DateTime2), CAST(N'2024-06-26T12:19:33.0400000' AS DateTime2))
GO
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'8d4a5bfb-10cb-4919-a028-07ea319707d4', N'44091e86-2ed5-4f88-b5f3-c39dadcdbe62', N'admin', CAST(N'2024-07-02T14:42:50.4419905' AS DateTime2), CAST(N'2024-06-25T14:42:50.4419906' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'74a39c6f-751d-466d-8975-0e0625d3639a', N'0e7241f5-c582-4576-b0ef-5722df135d89', N'admin', CAST(N'2024-07-02T14:42:49.5214190' AS DateTime2), CAST(N'2024-06-25T14:42:49.5214192' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'060227a7-1982-4b0d-a427-174465de0031', N'83fdcd27-9c45-4452-8a40-e1fea242e1da', N'admin', CAST(N'2024-07-03T00:33:00.0575154' AS DateTime2), CAST(N'2024-06-26T00:33:00.0575645' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'58a82fef-cf5f-4470-a4b5-1937a4d5b80e', N'72e65710-22bb-45c5-b762-db935ce05323', N'admin', CAST(N'2024-07-02T14:42:51.4326136' AS DateTime2), CAST(N'2024-06-25T14:42:51.4326142' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'b11dcfcf-6e0b-4416-830e-21033b1aa1f4', N'05eb95a4-dd08-4cae-99d3-b4b6e2236b4c', N'admin', CAST(N'2024-07-03T01:18:01.2276640' AS DateTime2), CAST(N'2024-06-26T01:18:01.2277092' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'b9f3c931-5c11-4483-b717-2c669e06fb00', N'95ac86c0-5556-4ad3-b5ad-46a2be99ecc4', N'admin', CAST(N'2024-07-02T14:42:52.2868435' AS DateTime2), CAST(N'2024-06-25T14:42:52.2868437' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'faa69a4e-24e7-42a6-b976-2c68b741244f', N'4338275f-94e4-4af4-ae2c-ac731d516024', N'admin', CAST(N'2024-07-03T03:00:56.5315093' AS DateTime2), CAST(N'2024-06-26T03:00:56.5315394' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'b3ab94c0-f44d-462b-b3fa-3a0ff7b7fbf7', N'515e6b56-de0c-4213-a11d-0914a10975c1', N'admin', CAST(N'2024-07-03T02:06:40.2153798' AS DateTime2), CAST(N'2024-06-26T02:06:40.2153806' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'b3751f57-c97d-422f-9cf7-3f44f73da999', N'812e13dd-7cd2-4e90-97f4-08e0b8ae618f', N'admin', CAST(N'2024-07-02T14:42:48.5580447' AS DateTime2), CAST(N'2024-06-25T14:42:48.5580450' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'3032b1ac-022f-4054-b5b1-4cdfeeddb842', N'b13cce2e-df2a-4053-a2ce-8627010be2c1', N'admin', CAST(N'2024-07-03T01:18:01.2276590' AS DateTime2), CAST(N'2024-06-26T01:18:01.2277028' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'0393cc38-27ff-461f-80ea-5358d2606181', N'f73baa44-2486-491e-ab3d-77e9560c90d4', N'admin', CAST(N'2024-07-02T14:23:42.4263794' AS DateTime2), CAST(N'2024-06-25T14:23:42.4264174' AS DateTime2), CAST(N'2024-06-25T14:24:20.8905804' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'2fd4f155-6304-472b-992a-578f1f5a4ea9', N'93fd5a7b-586b-48d3-b007-1bf75686465b', N'admin', CAST(N'2024-07-02T14:38:39.2299841' AS DateTime2), CAST(N'2024-06-25T14:38:39.2300085' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'975ed461-9383-45dd-8f0b-597e6304f30c', N'b3727d8d-62bf-4582-8340-8c547a1db85c', N'admin', CAST(N'2024-07-03T04:58:43.2159670' AS DateTime2), CAST(N'2024-06-26T04:58:43.2159676' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'e1801625-9937-4b12-9d2e-5be3d8238b33', N'8b18b8ba-5692-4805-a04b-be2b87179961', N'admin', CAST(N'2024-07-02T14:42:44.5148880' AS DateTime2), CAST(N'2024-06-25T14:42:44.5148884' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'0ce24ae1-11e0-4e78-95ae-719898cf7d69', N'bbcb8a46-9f70-4a02-864b-680366241f16', N'admin', CAST(N'2024-07-03T00:33:00.0829048' AS DateTime2), CAST(N'2024-06-26T00:33:00.0829053' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'f0637b1e-f0db-47a3-be2a-97d1d171e462', N'fd4aa5ab-d8bc-45c5-a3b9-15e7567bb665', N'admin', CAST(N'2024-07-02T14:44:10.9111912' AS DateTime2), CAST(N'2024-06-25T14:44:10.9111919' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'eeb68d71-b487-411b-9d28-9bc9b286c6eb', N'2f1bb3cd-7095-4afc-9d8e-40068f8d7b2f', N'admin', CAST(N'2024-07-03T12:55:28.4216332' AS DateTime2), CAST(N'2024-06-26T12:55:28.4216634' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'cecfbdfc-f96a-4bbb-8934-9e69c7c163b8', N'd78340ba-b118-4119-ba3f-5c78466d9304', N'admin', CAST(N'2024-07-02T14:42:53.1731905' AS DateTime2), CAST(N'2024-06-25T14:42:53.1731909' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'4d366cd2-6ae7-4398-a9d3-a80bd9533077', N'51850717-66a8-411a-8f8a-84d4cc0e7495', N'admin', CAST(N'2024-07-02T14:24:20.8814611' AS DateTime2), CAST(N'2024-06-25T14:24:20.8814619' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'045c34e2-2d4d-40c7-b8ce-ad5355395de9', N'4b73f5d2-0dff-4813-9008-1a52a6840278', N'admin', CAST(N'2024-07-02T14:45:28.2745976' AS DateTime2), CAST(N'2024-06-25T14:45:28.2745979' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'17572584-7162-42ea-8bd7-b7077edd6539', N'5b7b28c2-57ed-4517-949f-cc3950f8ffd4', N'admin', CAST(N'2024-07-02T14:41:16.1722020' AS DateTime2), CAST(N'2024-06-25T14:41:16.1722026' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'27845665-a3ab-463f-a462-b9e5cebe2df5', N'49693fe3-94c3-4cf7-b62f-24d5b32e8e71', N'admin', CAST(N'2024-07-02T14:42:47.5789386' AS DateTime2), CAST(N'2024-06-25T14:42:47.5789388' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'a9d5c52b-ce4a-467e-8cfc-c4bcca3c8c14', N'1997edee-9154-46dd-87a5-e32d015672cb', N'admin', CAST(N'2024-07-03T04:53:14.7825125' AS DateTime2), CAST(N'2024-06-26T04:53:14.7825499' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'39d67368-6267-4c1d-90c8-d625eaacc785', N'1b0c2249-f5de-4d60-887e-090687c39fa3', N'admin', CAST(N'2024-07-02T15:57:20.5201495' AS DateTime2), CAST(N'2024-06-25T15:57:20.5201500' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'e1d31373-57c4-4f12-add8-e8bcaa66c290', N'9f4ba767-51c7-42d5-87e3-04908ae74773', N'admin', CAST(N'2024-07-03T04:53:14.7825184' AS DateTime2), CAST(N'2024-06-26T04:53:14.7825569' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'fca49970-7dd9-4b12-abee-ec4f34ea5edb', N'9e986a27-bccd-4491-8232-c67ff57ecaf8', N'admin', CAST(N'2024-07-03T09:19:37.9543270' AS DateTime2), CAST(N'2024-06-26T09:19:37.9543714' AS DateTime2), NULL)
INSERT [dbo].[RefreshTokens] ([Id], [Token], [Username], [Expires], [Created], [Revoked]) VALUES (N'd9ad8418-514f-44e3-813b-ef6ca894ac87', N'6513eb45-9c66-4b91-bba7-789453094776', N'admin', CAST(N'2024-07-03T02:04:59.9728652' AS DateTime2), CAST(N'2024-06-26T02:04:59.9728983' AS DateTime2), NULL)
GO
SET IDENTITY_INSERT [dbo].[RoomAmenities] ON 

INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (3, N'f876ad20-60fd-4e72-9667-a415363f2e6c', N'2516223d-7606-4d72-baed-8301325d65d4')
INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (4, N'f876ad20-60fd-4e72-9667-a415363f2e6c', N'2bfd62c0-1ce2-4c75-ada7-beb59c6fb92a')
INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (5, N'f876ad20-60fd-4e72-9667-a415363f2e6c', N'9fd4c6bc-7407-4239-9bd4-6245727bcf98')
INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (6, N'c1ccf4a8-fba8-4306-8ca1-801e355769e0', N'9fd4c6bc-7407-4239-9bd4-6245727bcf98')
INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (7, N'c1ccf4a8-fba8-4306-8ca1-801e355769e0', N'e19a4265-bd79-44fc-998e-7036a8f775b9')
INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (8, N'c1ccf4a8-fba8-4306-8ca1-801e355769e0', N'019c55e6-9b60-43c5-be26-9b204c633a72')
INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (9, N'072b6c5f-0d20-406e-b48c-e760ba6c5931', N'9fd4c6bc-7407-4239-9bd4-6245727bcf98')
INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (10, N'072b6c5f-0d20-406e-b48c-e760ba6c5931', N'2516223d-7606-4d72-baed-8301325d65d4')
INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (11, N'072b6c5f-0d20-406e-b48c-e760ba6c5931', N'2bfd62c0-1ce2-4c75-ada7-beb59c6fb92a')
INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (12, N'072b6c5f-0d20-406e-b48c-e760ba6c5931', N'2bfd62c0-1ce2-4c75-ada7-beb59c6fb92a')
INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (14, N'e3a6c323-5e44-4de0-8c7c-4da988258985', N'ad5ad1ff-086a-48d1-a61c-624fca0769a3')
INSERT [dbo].[RoomAmenities] ([RoomAmenityId], [RoomId], [AmenityId]) VALUES (15, N'e3a6c323-5e44-4de0-8c7c-4da988258985', N'019c55e6-9b60-43c5-be26-9b204c633a72')
SET IDENTITY_INSERT [dbo].[RoomAmenities] OFF
GO
SET IDENTITY_INSERT [dbo].[RoomImages] ON 

INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (32, N'58d103b4-4da4-40db-b0a6-a334b5b595ed.jpg', CAST(N'2024-06-26T02:31:35.1696611' AS DateTime2), N'f876ad20-60fd-4e72-9667-a415363f2e6c')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (33, N'd1bf6020-1799-4fd3-9284-2cc3567cb398.jpg', CAST(N'2024-06-26T02:31:35.1740104' AS DateTime2), N'f876ad20-60fd-4e72-9667-a415363f2e6c')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (34, N'77796964-0f41-4f5a-916e-2df6b5311214.jpg', CAST(N'2024-06-26T02:31:35.1750020' AS DateTime2), N'f876ad20-60fd-4e72-9667-a415363f2e6c')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (35, N'340cf19c-234a-4c29-a266-fa1a72e81e76.jpg', CAST(N'2024-06-26T02:31:35.1762305' AS DateTime2), N'f876ad20-60fd-4e72-9667-a415363f2e6c')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (36, N'0473a940-a94e-44e3-823a-b91120ee6c31.jpg', CAST(N'2024-06-26T12:24:01.4338751' AS DateTime2), N'072b6c5f-0d20-406e-b48c-e760ba6c5931')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (37, N'f24f499d-1beb-472e-93d7-d7ea4c9de6dd.jpg', CAST(N'2024-06-26T12:24:01.4351212' AS DateTime2), N'072b6c5f-0d20-406e-b48c-e760ba6c5931')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (38, N'88ec352c-c8c0-4ee3-aa24-60e0faeef928.jpg', CAST(N'2024-06-26T12:24:01.4363650' AS DateTime2), N'072b6c5f-0d20-406e-b48c-e760ba6c5931')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (39, N'75f89389-92a4-42bf-b501-a4762dc0ac40.jpg', CAST(N'2024-06-26T12:24:01.4374504' AS DateTime2), N'072b6c5f-0d20-406e-b48c-e760ba6c5931')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (40, N'8dd2d427-280d-4d0c-9a5e-0e53867e66a8.jpg', CAST(N'2024-06-26T12:24:01.4385562' AS DateTime2), N'072b6c5f-0d20-406e-b48c-e760ba6c5931')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (41, N'58625081-102a-4d80-9b6b-e1458bab32fc.jpg', CAST(N'2024-06-26T12:24:01.4396133' AS DateTime2), N'072b6c5f-0d20-406e-b48c-e760ba6c5931')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (42, N'e0b07b69-4e11-46e4-aee4-b48529c5e4d1.jpg', CAST(N'2024-06-26T12:26:57.8573738' AS DateTime2), N'c1ccf4a8-fba8-4306-8ca1-801e355769e0')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (43, N'f42c0dc5-1237-4b22-bc31-7d8cd055b300.jpg', CAST(N'2024-06-26T12:26:57.8591370' AS DateTime2), N'c1ccf4a8-fba8-4306-8ca1-801e355769e0')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (44, N'efd7700f-825a-45f9-afe8-9631081135db.jpg', CAST(N'2024-06-26T12:26:57.8610297' AS DateTime2), N'c1ccf4a8-fba8-4306-8ca1-801e355769e0')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (45, N'5a84cf0a-2e1e-458e-b851-0bbbe5d57d6f.jpg', CAST(N'2024-06-26T12:26:57.8640451' AS DateTime2), N'c1ccf4a8-fba8-4306-8ca1-801e355769e0')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (46, N'36a048ec-a1dc-43a4-9bbe-585302e19b24.jpg', CAST(N'2024-06-26T12:26:57.8677672' AS DateTime2), N'c1ccf4a8-fba8-4306-8ca1-801e355769e0')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (47, N'f6592ea8-fcf7-408f-8608-5600bd8af4a5.jpg', CAST(N'2024-06-26T12:26:57.8718024' AS DateTime2), N'c1ccf4a8-fba8-4306-8ca1-801e355769e0')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (48, N'abb0c0c1-adfc-4985-83a9-43fd904f7eac.jpg', CAST(N'2024-06-26T12:29:00.6936213' AS DateTime2), N'e3a6c323-5e44-4de0-8c7c-4da988258985')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (49, N'e57aeb5d-c6ec-414d-b439-8cf70c50f050.jpg', CAST(N'2024-06-26T12:29:00.6948976' AS DateTime2), N'e3a6c323-5e44-4de0-8c7c-4da988258985')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (50, N'b25223eb-ce67-4588-a395-4fe1cc891279.jpg', CAST(N'2024-06-26T12:29:00.6959058' AS DateTime2), N'e3a6c323-5e44-4de0-8c7c-4da988258985')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (51, N'9cb3a8cc-cf1f-43ad-83d4-8c56c8532289.jpg', CAST(N'2024-06-26T12:29:00.6970413' AS DateTime2), N'e3a6c323-5e44-4de0-8c7c-4da988258985')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (52, N'ea40c1a5-3df5-4649-a79b-6befaa03879b.jpg', CAST(N'2024-06-26T12:29:00.6984500' AS DateTime2), N'e3a6c323-5e44-4de0-8c7c-4da988258985')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (53, N'b89b8aa1-67ac-4141-bb04-f1a094d86e14.jpg', CAST(N'2024-06-26T12:29:00.7000496' AS DateTime2), N'e3a6c323-5e44-4de0-8c7c-4da988258985')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (54, N'99fdcc62-c8cc-493b-87bb-ac1350f5f101.jpeg', CAST(N'2024-06-26T12:31:52.4104661' AS DateTime2), N'4a39e5c8-1db9-409b-bb14-ed5116dd3756')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (55, N'f3a8e0f7-f5b4-4e37-a0b1-9c53e699b866.jpeg', CAST(N'2024-06-26T12:31:52.4149297' AS DateTime2), N'4a39e5c8-1db9-409b-bb14-ed5116dd3756')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (56, N'6af57390-6e3e-4903-bec8-92c8aa72fef0.jpeg', CAST(N'2024-06-26T12:31:52.4169805' AS DateTime2), N'4a39e5c8-1db9-409b-bb14-ed5116dd3756')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (57, N'ce533c14-72fd-4be3-81de-f5e02f7d82a3.jpg', CAST(N'2024-06-26T12:33:47.4101443' AS DateTime2), N'513e8181-32b7-46e0-ac93-7a0091fdaf4a')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (58, N'42c50c82-78bb-4666-ad42-82efad2008cb.jpg', CAST(N'2024-06-26T12:33:47.4118806' AS DateTime2), N'513e8181-32b7-46e0-ac93-7a0091fdaf4a')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (59, N'fcfa8c41-61d3-4064-b250-8f55a1d90e32.jpg', CAST(N'2024-06-26T12:33:47.4142679' AS DateTime2), N'513e8181-32b7-46e0-ac93-7a0091fdaf4a')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (60, N'f7e9efa3-366d-4c37-a633-bd792e1a96bb.jpg', CAST(N'2024-06-26T12:33:47.4155116' AS DateTime2), N'513e8181-32b7-46e0-ac93-7a0091fdaf4a')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (61, N'ce8a99f8-21e3-489b-b9df-ffa24e151810.jpg', CAST(N'2024-06-26T12:33:47.4166781' AS DateTime2), N'513e8181-32b7-46e0-ac93-7a0091fdaf4a')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (62, N'54bc282a-ceaa-4799-be68-65dc03c06deb.jpg', CAST(N'2024-06-26T12:33:47.4180190' AS DateTime2), N'513e8181-32b7-46e0-ac93-7a0091fdaf4a')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (63, N'b2bb25a9-4d28-41ca-bc34-d32bf525cbcf.jpg', CAST(N'2024-06-26T12:33:47.4190382' AS DateTime2), N'513e8181-32b7-46e0-ac93-7a0091fdaf4a')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (64, N'0425d39c-9e78-463e-93b4-fb898bbb4ed1.jpg', CAST(N'2024-06-26T12:33:47.4199618' AS DateTime2), N'513e8181-32b7-46e0-ac93-7a0091fdaf4a')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (65, N'9062a39a-0f05-49a7-9b5f-25fdbc26b342.jpg', CAST(N'2024-06-26T12:33:47.4213089' AS DateTime2), N'513e8181-32b7-46e0-ac93-7a0091fdaf4a')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (66, N'7b787d81-0e4f-4f24-9bbd-e0875601d1b2.jpg', CAST(N'2024-06-26T12:35:36.3018380' AS DateTime2), N'28e00fac-ed98-4a61-ad47-8e8fb63221b9')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (67, N'd0bd1043-6c67-41d0-927f-136e9b77ed16.jpg', CAST(N'2024-06-26T12:35:36.3032395' AS DateTime2), N'28e00fac-ed98-4a61-ad47-8e8fb63221b9')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (68, N'bd227f37-00c4-4eb7-9eff-6b94f8484298.jpg', CAST(N'2024-06-26T12:35:36.3047319' AS DateTime2), N'28e00fac-ed98-4a61-ad47-8e8fb63221b9')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (69, N'c9438ac5-7f4b-4ede-aeca-322881076ab5.jpg', CAST(N'2024-06-26T12:35:36.3058319' AS DateTime2), N'28e00fac-ed98-4a61-ad47-8e8fb63221b9')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (70, N'afb901ae-9d2c-4d03-82e1-a38c88d2b654.jpg', CAST(N'2024-06-26T12:35:36.3071546' AS DateTime2), N'28e00fac-ed98-4a61-ad47-8e8fb63221b9')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (71, N'e69bbdb5-4a13-4b72-9ffa-bdc3dd145e87.jpg', CAST(N'2024-06-26T12:35:36.3081711' AS DateTime2), N'28e00fac-ed98-4a61-ad47-8e8fb63221b9')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (72, N'c6df2950-1f9b-42fe-9684-56834506d52f.jpg', CAST(N'2024-06-26T12:35:36.3098349' AS DateTime2), N'28e00fac-ed98-4a61-ad47-8e8fb63221b9')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (73, N'e7920a07-b3e2-47f1-9c54-9eacdc4086b0.jpg', CAST(N'2024-06-26T12:37:39.7928321' AS DateTime2), N'10131085-839d-4f2e-b4b3-1a67302b4164')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (74, N'8971285f-d0c8-4f36-82fb-71bd434d8e88.jpg', CAST(N'2024-06-26T12:37:39.7939582' AS DateTime2), N'10131085-839d-4f2e-b4b3-1a67302b4164')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (75, N'15c10961-026d-415d-950a-cbee7f9196a5.jpg', CAST(N'2024-06-26T12:37:39.7949503' AS DateTime2), N'10131085-839d-4f2e-b4b3-1a67302b4164')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (76, N'04ac2403-eabb-4c9e-acc2-91372b0e1687.jpg', CAST(N'2024-06-26T12:37:39.7959159' AS DateTime2), N'10131085-839d-4f2e-b4b3-1a67302b4164')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (77, N'63c0632f-4422-4df5-8143-1b15728e71de.jpg', CAST(N'2024-06-26T12:37:39.7969999' AS DateTime2), N'10131085-839d-4f2e-b4b3-1a67302b4164')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (78, N'fea77bbe-48d9-4b9b-8910-716d1ae7c49b.jpg', CAST(N'2024-06-26T12:37:39.7984264' AS DateTime2), N'10131085-839d-4f2e-b4b3-1a67302b4164')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (79, N'c4de2ddd-333f-4e5d-83ef-59d131f8664c.jpg', CAST(N'2024-06-26T12:37:39.8004298' AS DateTime2), N'10131085-839d-4f2e-b4b3-1a67302b4164')
INSERT [dbo].[RoomImages] ([RoomImageId], [ImageUrl], [CreatedAt], [RoomId]) VALUES (80, N'7addf842-5d00-41ec-b1e7-fda868ace88d.jpg', CAST(N'2024-06-26T12:37:39.8016497' AS DateTime2), N'10131085-839d-4f2e-b4b3-1a67302b4164')
SET IDENTITY_INSERT [dbo].[RoomImages] OFF
GO
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Price], [RoomCount], [Area], [NumberPerson], [View], [EatBreakfast], [BedDescription], [HotelId], [CreatedAt], [UpdatedAt]) VALUES (N'10131085-839d-4f2e-b4b3-1a67302b4164', N'Phòng dành cho gia đình', 1200000, 11, 60, 4, N'Không có', 0, N'2 giường queen', N'ae851a0b-3c47-4856-0dda-08dc959f9017', CAST(N'2024-06-26T05:37:39.7908792' AS DateTime2), CAST(N'2024-06-26T12:37:39.8133333' AS DateTime2))
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Price], [RoomCount], [Area], [NumberPerson], [View], [EatBreakfast], [BedDescription], [HotelId], [CreatedAt], [UpdatedAt]) VALUES (N'e3a6c323-5e44-4de0-8c7c-4da988258985', N'Phòng Deluxe', 728000, 33, 25, 1, N'Tầm nhìn hướng thành phố', 1, N'2 giường đơn hoặc 1 giường đôi hoặc 1 giường queen', N'99503832-3b04-41e5-a428-08dc951e787a', CAST(N'2024-06-26T05:29:00.6919682' AS DateTime2), CAST(N'2024-06-26T12:29:00.7166667' AS DateTime2))
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Price], [RoomCount], [Area], [NumberPerson], [View], [EatBreakfast], [BedDescription], [HotelId], [CreatedAt], [UpdatedAt]) VALUES (N'513e8181-32b7-46e0-ac93-7a0091fdaf4a', N'Phòng Deluxe', 600000, 22, 18, 2, N'Chưa có', 0, N'1 giường queen', N'ae851a0b-3c47-4856-0dda-08dc959f9017', CAST(N'2024-06-26T05:33:47.4081345' AS DateTime2), CAST(N'2024-06-26T12:33:47.4300000' AS DateTime2))
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Price], [RoomCount], [Area], [NumberPerson], [View], [EatBreakfast], [BedDescription], [HotelId], [CreatedAt], [UpdatedAt]) VALUES (N'c1ccf4a8-fba8-4306-8ca1-801e355769e0', N'Phòng dành cho gia đình', 1984000, 33, 60, 2, N'Tầm nhìn hướng thành phố', 1, N'Loại giường sẽ được sắp xếp lúc nhận phòng', N'99503832-3b04-41e5-a428-08dc951e787a', CAST(N'2024-06-26T05:26:57.8542270' AS DateTime2), CAST(N'2024-06-26T12:26:57.8900000' AS DateTime2))
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Price], [RoomCount], [Area], [NumberPerson], [View], [EatBreakfast], [BedDescription], [HotelId], [CreatedAt], [UpdatedAt]) VALUES (N'28e00fac-ed98-4a61-ad47-8e8fb63221b9', N'Phòng Executive', 750000, 11, 25, 2, N'Tầm nhìn hướng thành phố', 0, N'1 giường queen', N'ae851a0b-3c47-4856-0dda-08dc959f9017', CAST(N'2024-06-26T05:35:36.2999887' AS DateTime2), CAST(N'2024-06-26T12:35:36.3200000' AS DateTime2))
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Price], [RoomCount], [Area], [NumberPerson], [View], [EatBreakfast], [BedDescription], [HotelId], [CreatedAt], [UpdatedAt]) VALUES (N'f876ad20-60fd-4e72-9667-a415363f2e6c', N'Phòng Tiêu chuẩn', 820000, 5, 18, 2, N'Không có', 1, N'1 giường queen', N'99503832-3b04-41e5-a428-08dc951e787a', CAST(N'2024-06-25T14:21:31.0190287' AS DateTime2), CAST(N'2024-06-25T21:21:31.0300000' AS DateTime2))
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Price], [RoomCount], [Area], [NumberPerson], [View], [EatBreakfast], [BedDescription], [HotelId], [CreatedAt], [UpdatedAt]) VALUES (N'072b6c5f-0d20-406e-b48c-e760ba6c5931', N'Phòng đôi hoặc 2 giường đơn Junior', 730000, 44, 32, 2, N'Tầm nhìn hướng thành phố', 1, N'Loại giường sẽ được sắp xếp lúc check-in', N'99503832-3b04-41e5-a428-08dc951e787a', CAST(N'2024-06-26T05:24:01.4297726' AS DateTime2), CAST(N'2024-06-26T12:24:01.4666667' AS DateTime2))
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Price], [RoomCount], [Area], [NumberPerson], [View], [EatBreakfast], [BedDescription], [HotelId], [CreatedAt], [UpdatedAt]) VALUES (N'4a39e5c8-1db9-409b-bb14-ed5116dd3756', N'Phòng Đôi - Ở Trong Ngày, Ở Tối Đa 2 Tiếng', 220000, 33, 18, 2, N'Chưa có', 0, N'1 giường đôi', N'ae851a0b-3c47-4856-0dda-08dc959f9017', CAST(N'2024-06-26T05:31:52.4085459' AS DateTime2), CAST(N'2024-06-26T12:31:52.4233333' AS DateTime2))
GO
INSERT [dbo].[Users] ([UserId], [Username], [HashPassword], [FullName], [PhoneNumber], [Email], [BirthDate], [Country], [Role], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'cedfa522-6901-4324-be56-4c16d1f01e19', N'admin', N'E10ADC3949BA59ABBE56E057F20F883E', N'Người quản lý', N'0123456789', N'admin@example.com', CAST(N'1980-01-01' AS Date), N'Vietnam', 0, 0, CAST(N'2024-06-25T13:48:22.2840643' AS DateTime2), CAST(N'2024-06-25T20:49:49.6133333' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Username], [HashPassword], [FullName], [PhoneNumber], [Email], [BirthDate], [Country], [Role], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'97f22263-f26f-494a-a8da-8d9dd49f6c9e', N'fsdafdsa', N'UgPdZbk3BS/1oesv82e9uJpp2P5z3s942iLhh92gftY=', N'fdsafdsa', N'43214312', N'fdsasdfa@gmail.com', CAST(N'2024-06-19' AS Date), N'fdwsafdsa', 2, 0, CAST(N'2024-06-26T13:06:35.5462659' AS DateTime2), CAST(N'2024-06-26T20:06:35.6966667' AS DateTime2))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_BookingDetails_BookingId]    Script Date: 6/26/2024 10:01:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_BookingDetails_BookingId] ON [dbo].[BookingDetails]
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BookingDetails_RoomId]    Script Date: 6/26/2024 10:01:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_BookingDetails_RoomId] ON [dbo].[BookingDetails]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bookings_UserId]    Script Date: 6/26/2024 10:01:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_Bookings_UserId] ON [dbo].[Bookings]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HotelImages_HotelId]    Script Date: 6/26/2024 10:01:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_HotelImages_HotelId] ON [dbo].[HotelImages]
(
	[HotelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_RoomId]    Script Date: 6/26/2024 10:01:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_RoomId] ON [dbo].[Reviews]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_UserId]    Script Date: 6/26/2024 10:01:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_UserId] ON [dbo].[Reviews]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoomAmenities_AmenityId]    Script Date: 6/26/2024 10:01:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoomAmenities_AmenityId] ON [dbo].[RoomAmenities]
(
	[AmenityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoomAmenities_RoomId]    Script Date: 6/26/2024 10:01:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoomAmenities_RoomId] ON [dbo].[RoomAmenities]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoomImages_RoomId]    Script Date: 6/26/2024 10:01:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoomImages_RoomId] ON [dbo].[RoomImages]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Rooms_HotelId]    Script Date: 6/26/2024 10:01:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_Rooms_HotelId] ON [dbo].[Rooms]
(
	[HotelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Bookings] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[HotelImages] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Hotels] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Hotels] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Reviews] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[RoomImages] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Rooms] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Rooms] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD  CONSTRAINT [FK_BookingDetail_Booking] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Bookings] ([BookingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookingDetails] CHECK CONSTRAINT [FK_BookingDetail_Booking]
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD  CONSTRAINT [FK_BookingDetail_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([RoomId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookingDetails] CHECK CONSTRAINT [FK_BookingDetail_Room]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Booking_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Booking_User]
GO
ALTER TABLE [dbo].[HotelImages]  WITH CHECK ADD  CONSTRAINT [FK_HotelImage_Hotel] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotels] ([HotelId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HotelImages] CHECK CONSTRAINT [FK_HotelImage_Hotel]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Review_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([RoomId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Review_Room]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Review_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Review_User]
GO
ALTER TABLE [dbo].[RoomAmenities]  WITH CHECK ADD  CONSTRAINT [FK_RoomAmenity_Amenity] FOREIGN KEY([AmenityId])
REFERENCES [dbo].[Amenities] ([AmenityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoomAmenities] CHECK CONSTRAINT [FK_RoomAmenity_Amenity]
GO
ALTER TABLE [dbo].[RoomAmenities]  WITH CHECK ADD  CONSTRAINT [FK_RoomAmenity_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([RoomId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoomAmenities] CHECK CONSTRAINT [FK_RoomAmenity_Room]
GO
ALTER TABLE [dbo].[RoomImages]  WITH CHECK ADD  CONSTRAINT [FK_RoomImages_Rooms_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([RoomId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoomImages] CHECK CONSTRAINT [FK_RoomImages_Rooms_RoomId]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_Hotels_HotelId] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotels] ([HotelId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Rooms_Hotels_HotelId]
GO
USE [master]
GO
ALTER DATABASE [HotelBooking] SET  READ_WRITE 
GO

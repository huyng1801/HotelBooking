using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("cedfa522-6901-4324-be56-4c16d1f01e19"));

            migrationBuilder.AlterColumn<long>(
                name: "TotalAmount",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "BirthDate", "Country", "CreatedAt", "Email", "FullName", "HashPassword", "IsActive", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("24afdebf-9c92-4573-82ce-a377c06f7ba2"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vietnam", new DateTime(2024, 6, 27, 6, 55, 31, 758, DateTimeKind.Utc).AddTicks(4262), "admin@example.com", "Người quản lý", "E10ADC3949BA59ABBE56E057F20F883E", true, "0123456789", 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("24afdebf-9c92-4573-82ce-a377c06f7ba2"));

            migrationBuilder.AlterColumn<int>(
                name: "TotalAmount",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "BirthDate", "Country", "CreatedAt", "Email", "FullName", "HashPassword", "IsActive", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("cedfa522-6901-4324-be56-4c16d1f01e19"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vietnam", new DateTime(2024, 6, 25, 13, 48, 22, 284, DateTimeKind.Utc).AddTicks(643), "admin@example.com", "Người quản lý", "E10ADC3949BA59ABBE56E057F20F883E", true, "0123456789", 0, "admin" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResortBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDetails1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Resorts",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageUrl", "Name", "Occupancy", "Price", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 2, null, "It is a beautiful, premium resort with good and nice free space.", "https://placehold.co/600x400", "Premium Resort", 4, 200.0, 550, null },
                    { 3, null, "It is a beautiful and super as a good budget friendly resort with good and nice free space.", "https://placehold.co/600x400", "Super Resort", 4, 200.0, 550, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resorts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Resorts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

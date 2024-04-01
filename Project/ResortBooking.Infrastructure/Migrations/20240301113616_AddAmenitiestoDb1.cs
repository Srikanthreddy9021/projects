using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResortBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAmenitiestoDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 402);

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Description", "Name", "ResortId" },
                values: new object[,]
                {
                    { 1, null, "Table fan", 1 },
                    { 2, null, "1 King bed", 1 },
                    { 3, null, "3 sofa beds", 1 },
                    { 4, null, "Mini Referigerator", 1 },
                    { 5, null, "2 Double beds", 1 },
                    { 6, null, "Private plunge pool", 2 },
                    { 7, null, "Private Balcony", 2 },
                    { 8, null, "Referigerator", 3 },
                    { 9, null, "Microwave", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.InsertData(
                table: "ResortNumbers",
                columns: new[] { "Resort_Number", "ResortId", "SpecialDetails" },
                values: new object[] { 402, 4, null });
        }
    }
}

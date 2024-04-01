using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDetails2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Resorts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Occupancy", "Price", "Sqft" },
                values: new object[] { 6, 500.0, 750 });

            migrationBuilder.UpdateData(
                table: "Resorts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Occupancy", "Price", "Sqft" },
                values: new object[] { 5, 450.0, 650 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Resorts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Occupancy", "Price", "Sqft" },
                values: new object[] { 4, 200.0, 550 });

            migrationBuilder.UpdateData(
                table: "Resorts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Occupancy", "Price", "Sqft" },
                values: new object[] { 4, 200.0, 550 });
        }
    }
}

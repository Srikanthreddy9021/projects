using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResortBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddResortNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialDetails",
                table: "ResortNumbers");

            migrationBuilder.RenameColumn(
                name: "Villa_Number",
                table: "ResortNumbers",
                newName: "Resort_Number");

            migrationBuilder.InsertData(
                table: "ResortNumbers",
                columns: new[] { "Resort_Number", "ResortId" },
                values: new object[,]
                {
                    { 101, 1 },
                    { 102, 1 },
                    { 103, 1 },
                    { 201, 2 },
                    { 202, 2 },
                    { 301, 3 },
                    { 302, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 302);

            migrationBuilder.RenameColumn(
                name: "Resort_Number",
                table: "ResortNumbers",
                newName: "Villa_Number");

            migrationBuilder.AddColumn<string>(
                name: "SpecialDetails",
                table: "ResortNumbers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

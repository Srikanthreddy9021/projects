using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeResortNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpecialDetails",
                table: "ResortNumbers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 101,
                column: "SpecialDetails",
                value: null);

            migrationBuilder.UpdateData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 102,
                column: "SpecialDetails",
                value: null);

            migrationBuilder.UpdateData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 103,
                column: "SpecialDetails",
                value: null);

            migrationBuilder.UpdateData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 201,
                column: "SpecialDetails",
                value: null);

            migrationBuilder.UpdateData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 202,
                column: "SpecialDetails",
                value: null);

            migrationBuilder.UpdateData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 301,
                column: "SpecialDetails",
                value: null);

            migrationBuilder.UpdateData(
                table: "ResortNumbers",
                keyColumn: "Resort_Number",
                keyValue: 302,
                column: "SpecialDetails",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialDetails",
                table: "ResortNumbers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCookies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Resorts_VillaId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "VillaId",
                table: "Bookings",
                newName: "ResortId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_VillaId",
                table: "Bookings",
                newName: "IX_Bookings_ResortId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CheckOutDate",
                table: "Bookings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CheckInDate",
                table: "Bookings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Resorts_ResortId",
                table: "Bookings",
                column: "ResortId",
                principalTable: "Resorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Resorts_ResortId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ResortId",
                table: "Bookings",
                newName: "VillaId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ResortId",
                table: "Bookings",
                newName: "IX_Bookings_VillaId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOutDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Resorts_VillaId",
                table: "Bookings",
                column: "VillaId",
                principalTable: "Resorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

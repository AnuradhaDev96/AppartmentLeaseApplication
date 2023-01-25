using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppartmentLeaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToInquiryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ReservationInquiries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ReservationInquiries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ReservationInquiries");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ReservationInquiries");
        }
    }
}

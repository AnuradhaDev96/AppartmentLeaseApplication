using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppartmentLeaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnInApartmentClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomCount",
                table: "ApartmentClasses",
                newName: "BedCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BedCount",
                table: "ApartmentClasses",
                newName: "RoomCount");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppartmentLeaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBuildingRelationshipWithWaitingApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaitingApplications_Buildings_BuildingId",
                table: "WaitingApplications");

            migrationBuilder.DropIndex(
                name: "IX_WaitingApplications_BuildingId",
                table: "WaitingApplications");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "WaitingApplications");

            migrationBuilder.AddColumn<string>(
                name: "RequiredBuildingLocation",
                table: "WaitingApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredBuildingLocation",
                table: "WaitingApplications");

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "WaitingApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WaitingApplications_BuildingId",
                table: "WaitingApplications",
                column: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_WaitingApplications_Buildings_BuildingId",
                table: "WaitingApplications",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppartmentLeaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddOccupantIdToLeasgreement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChiefOccupantId",
                table: "LeaseAgreements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LeaseAgreements_ChiefOccupantId",
                table: "LeaseAgreements",
                column: "ChiefOccupantId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseAgreements_ChiefOccupants_ChiefOccupantId",
                table: "LeaseAgreements",
                column: "ChiefOccupantId",
                principalTable: "ChiefOccupants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaseAgreements_ChiefOccupants_ChiefOccupantId",
                table: "LeaseAgreements");

            migrationBuilder.DropIndex(
                name: "IX_LeaseAgreements_ChiefOccupantId",
                table: "LeaseAgreements");

            migrationBuilder.DropColumn(
                name: "ChiefOccupantId",
                table: "LeaseAgreements");
        }
    }
}

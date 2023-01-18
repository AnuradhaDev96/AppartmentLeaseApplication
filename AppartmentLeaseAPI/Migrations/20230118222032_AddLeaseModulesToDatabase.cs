using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppartmentLeaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaseModulesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PricePerMonth",
                table: "ApartmentClasses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RefundableDepositAmount",
                table: "ApartmentClasses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "ChiefOccupants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NICPassportNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiefOccupants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiefOccupants_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaseAgreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasedParkingSpaceId = table.Column<int>(type: "int", nullable: true),
                    ApartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseAgreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaseAgreements_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaseAgreements_ParkingSpaces_PurchasedParkingSpaceId",
                        column: x => x.PurchasedParkingSpaceId,
                        principalTable: "ParkingSpaces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Dependants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChiefOccupantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependants_ChiefOccupants_ChiefOccupantId",
                        column: x => x.ChiefOccupantId,
                        principalTable: "ChiefOccupants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaseExtentionRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaseAgreementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseExtentionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaseExtentionRequests_LeaseAgreements_LeaseAgreementId",
                        column: x => x.LeaseAgreementId,
                        principalTable: "LeaseAgreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiefOccupants_SystemUserId",
                table: "ChiefOccupants",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependants_ChiefOccupantId",
                table: "Dependants",
                column: "ChiefOccupantId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseAgreements_ApartmentId",
                table: "LeaseAgreements",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseAgreements_PurchasedParkingSpaceId",
                table: "LeaseAgreements",
                column: "PurchasedParkingSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseExtentionRequests_LeaseAgreementId",
                table: "LeaseExtentionRequests",
                column: "LeaseAgreementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependants");

            migrationBuilder.DropTable(
                name: "LeaseExtentionRequests");

            migrationBuilder.DropTable(
                name: "ChiefOccupants");

            migrationBuilder.DropTable(
                name: "LeaseAgreements");

            migrationBuilder.DropColumn(
                name: "PricePerMonth",
                table: "ApartmentClasses");

            migrationBuilder.DropColumn(
                name: "RefundableDepositAmount",
                table: "ApartmentClasses");
        }
    }
}

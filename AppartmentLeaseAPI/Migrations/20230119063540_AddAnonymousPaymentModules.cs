using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppartmentLeaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAnonymousPaymentModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentInstallments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaseAgreementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInstallments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentInstallments_LeaseAgreements_LeaseAgreementId",
                        column: x => x.LeaseAgreementId,
                        principalTable: "LeaseAgreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaseAgreementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_LeaseAgreements_LeaseAgreementId",
                        column: x => x.LeaseAgreementId,
                        principalTable: "LeaseAgreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaitingApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApartmentClassId = table.Column<int>(type: "int", nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaitingApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaitingApplications_ApartmentClasses_ApartmentClassId",
                        column: x => x.ApartmentClassId,
                        principalTable: "ApartmentClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WaitingApplications_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationInquiries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaitingApplicationId = table.Column<int>(type: "int", nullable: true),
                    LeaseAgreementId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationInquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationInquiries_LeaseAgreements_LeaseAgreementId",
                        column: x => x.LeaseAgreementId,
                        principalTable: "LeaseAgreements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReservationInquiries_WaitingApplications_WaitingApplicationId",
                        column: x => x.WaitingApplicationId,
                        principalTable: "WaitingApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInstallments_LeaseAgreementId",
                table: "PaymentInstallments",
                column: "LeaseAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LeaseAgreementId",
                table: "Payments",
                column: "LeaseAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationInquiries_LeaseAgreementId",
                table: "ReservationInquiries",
                column: "LeaseAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationInquiries_WaitingApplicationId",
                table: "ReservationInquiries",
                column: "WaitingApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingApplications_ApartmentClassId",
                table: "WaitingApplications",
                column: "ApartmentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingApplications_BuildingId",
                table: "WaitingApplications",
                column: "BuildingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentInstallments");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ReservationInquiries");

            migrationBuilder.DropTable(
                name: "WaitingApplications");
        }
    }
}

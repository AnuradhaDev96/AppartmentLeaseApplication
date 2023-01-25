using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppartmentLeaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingFieldsToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RequiredStartDate",
                table: "WaitingApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidOn",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "PaymentInstallments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PayOrder",
                table: "PaymentInstallments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndtDate",
                table: "LeaseAgreements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsMonthAdvancePaid",
                table: "LeaseAgreements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRefundableDepositPaid",
                table: "LeaseAgreements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "LeaseAgreements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "TotalCost",
                table: "LeaseAgreements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredStartDate",
                table: "WaitingApplications");

            migrationBuilder.DropColumn(
                name: "PaidOn",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "PaymentInstallments");

            migrationBuilder.DropColumn(
                name: "PayOrder",
                table: "PaymentInstallments");

            migrationBuilder.DropColumn(
                name: "EndtDate",
                table: "LeaseAgreements");

            migrationBuilder.DropColumn(
                name: "IsMonthAdvancePaid",
                table: "LeaseAgreements");

            migrationBuilder.DropColumn(
                name: "IsRefundableDepositPaid",
                table: "LeaseAgreements");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "LeaseAgreements");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "LeaseAgreements");
        }
    }
}

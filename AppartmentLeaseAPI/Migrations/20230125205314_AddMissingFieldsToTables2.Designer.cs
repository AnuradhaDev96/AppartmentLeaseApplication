﻿// <auto-generated />
using System;
using AppartmentLeaseAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppartmentLeaseAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230125205314_AddMissingFieldsToTables2")]
    partial class AddMissingFieldsToTables2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Anonymous.ReservationInquiry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LeaseAgreementId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WaitingApplicationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeaseAgreementId");

                    b.HasIndex("WaitingApplicationId");

                    b.ToTable("ReservationInquiries");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Anonymous.WaitingApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApartmentClassId")
                        .HasColumnType("int");

                    b.Property<int>("BuildingId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequiredStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TelephoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentClassId");

                    b.HasIndex("BuildingId");

                    b.ToTable("WaitingApplications");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Apartments.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApartmentClassId")
                        .HasColumnType("int");

                    b.Property<int>("BuildingId")
                        .HasColumnType("int");

                    b.Property<int>("ParkingSpaceId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentClassId");

                    b.HasIndex("BuildingId");

                    b.HasIndex("ParkingSpaceId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Apartments.ApartmentClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttachedBathroomCount")
                        .HasColumnType("int");

                    b.Property<int>("BedCount")
                        .HasColumnType("int");

                    b.Property<int>("MaximumOccupantCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerMonth")
                        .HasColumnType("float");

                    b.Property<double>("RefundableDepositAmount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("ApartmentClasses");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Apartments.ApartmentClassFacility", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("ApartmentClassId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "ApartmentClassId");

                    b.HasIndex("ApartmentClassId");

                    b.ToTable("ApartmentClassFacilities");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Apartments.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Apartments.ParkingSpace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LotNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ParkingSpaces");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Customers.ChiefOccupant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyContactNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NICPassportNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SystemUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SystemUserId");

                    b.ToTable("ChiefOccupants");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Customers.Dependant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChiefOccupantId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChiefOccupantId");

                    b.ToTable("Dependants");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.LeaseManagement.LeaseAgreement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApartmentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndtDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMonthAdvancePaid")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRefundableDepositPaid")
                        .HasColumnType("bit");

                    b.Property<int?>("PurchasedParkingSpaceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalCost")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("PurchasedParkingSpaceId");

                    b.ToTable("LeaseAgreements");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.LeaseManagement.LeaseExtentionRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LeaseAgreementId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LeaseAgreementId");

                    b.ToTable("LeaseExtentionRequests");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.LeaseManagement.PaymentInstallment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LeaseAgreementId")
                        .HasColumnType("int");

                    b.Property<int>("PayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LeaseAgreementId");

                    b.ToTable("PaymentInstallments");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Payments.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LeaseAgreementId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaidOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LeaseAgreementId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SystemUsers");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Anonymous.ReservationInquiry", b =>
                {
                    b.HasOne("AppartmentLeaseAPI.Models.LeaseManagement.LeaseAgreement", "LeaseAgreement")
                        .WithMany()
                        .HasForeignKey("LeaseAgreementId");

                    b.HasOne("AppartmentLeaseAPI.Models.Anonymous.WaitingApplication", "WaitingApplication")
                        .WithMany()
                        .HasForeignKey("WaitingApplicationId");

                    b.Navigation("LeaseAgreement");

                    b.Navigation("WaitingApplication");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Anonymous.WaitingApplication", b =>
                {
                    b.HasOne("AppartmentLeaseAPI.Models.Apartments.ApartmentClass", "ApartmentClass")
                        .WithMany()
                        .HasForeignKey("ApartmentClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppartmentLeaseAPI.Models.Apartments.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApartmentClass");

                    b.Navigation("Building");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Apartments.Apartment", b =>
                {
                    b.HasOne("AppartmentLeaseAPI.Models.Apartments.ApartmentClass", "ApartmentClass")
                        .WithMany("Apartments")
                        .HasForeignKey("ApartmentClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppartmentLeaseAPI.Models.Apartments.Building", "Building")
                        .WithMany("Apartments")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppartmentLeaseAPI.Models.Apartments.ParkingSpace", "ParkingSpace")
                        .WithMany()
                        .HasForeignKey("ParkingSpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApartmentClass");

                    b.Navigation("Building");

                    b.Navigation("ParkingSpace");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Apartments.ApartmentClassFacility", b =>
                {
                    b.HasOne("AppartmentLeaseAPI.Models.Apartments.ApartmentClass", "ApartmentClass")
                        .WithMany("Facilities")
                        .HasForeignKey("ApartmentClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApartmentClass");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Customers.ChiefOccupant", b =>
                {
                    b.HasOne("AppartmentLeaseAPI.Models.UserModel", "SystemUser")
                        .WithMany()
                        .HasForeignKey("SystemUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SystemUser");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Customers.Dependant", b =>
                {
                    b.HasOne("AppartmentLeaseAPI.Models.Customers.ChiefOccupant", "ChiefOccupant")
                        .WithMany("Dependants")
                        .HasForeignKey("ChiefOccupantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiefOccupant");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.LeaseManagement.LeaseAgreement", b =>
                {
                    b.HasOne("AppartmentLeaseAPI.Models.Apartments.Apartment", "Apartment")
                        .WithMany()
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppartmentLeaseAPI.Models.Apartments.ParkingSpace", "PurchasedParkingSpace")
                        .WithMany()
                        .HasForeignKey("PurchasedParkingSpaceId");

                    b.Navigation("Apartment");

                    b.Navigation("PurchasedParkingSpace");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.LeaseManagement.LeaseExtentionRequest", b =>
                {
                    b.HasOne("AppartmentLeaseAPI.Models.LeaseManagement.LeaseAgreement", "LeaseAgreement")
                        .WithMany("LeaseExtentionRequests")
                        .HasForeignKey("LeaseAgreementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeaseAgreement");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.LeaseManagement.PaymentInstallment", b =>
                {
                    b.HasOne("AppartmentLeaseAPI.Models.LeaseManagement.LeaseAgreement", "LeaseAgreement")
                        .WithMany("PaymentInstallments")
                        .HasForeignKey("LeaseAgreementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeaseAgreement");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Payments.Payment", b =>
                {
                    b.HasOne("AppartmentLeaseAPI.Models.LeaseManagement.LeaseAgreement", "LeaseAgreement")
                        .WithMany("DonePayments")
                        .HasForeignKey("LeaseAgreementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeaseAgreement");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Apartments.ApartmentClass", b =>
                {
                    b.Navigation("Apartments");

                    b.Navigation("Facilities");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Apartments.Building", b =>
                {
                    b.Navigation("Apartments");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.Customers.ChiefOccupant", b =>
                {
                    b.Navigation("Dependants");
                });

            modelBuilder.Entity("AppartmentLeaseAPI.Models.LeaseManagement.LeaseAgreement", b =>
                {
                    b.Navigation("DonePayments");

                    b.Navigation("LeaseExtentionRequests");

                    b.Navigation("PaymentInstallments");
                });
#pragma warning restore 612, 618
        }
    }
}

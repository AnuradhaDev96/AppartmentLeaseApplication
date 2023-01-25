using AppartmentLeaseAPI.Data.Enums;
using AppartmentLeaseAPI.Models;
using AppartmentLeaseAPI.Models.Anonymous;
using AppartmentLeaseAPI.Models.Apartments;
using AppartmentLeaseAPI.Models.Customers;
using AppartmentLeaseAPI.Models.LeaseManagement;
using AppartmentLeaseAPI.Models.Payments;
using Microsoft.EntityFrameworkCore;

namespace AppartmentLeaseAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserModel> SystemUsers { get; set; }

        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<ApartmentClass> ApartmentClasses { get; set; }

        public DbSet<ApartmentClassFacility> ApartmentClassFacilities { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<ParkingSpace> ParkingSpaces { get; set; }

        public DbSet<ChiefOccupant> ChiefOccupants { get; set; }
        public DbSet<Dependant> Dependants { get; set; }
        public DbSet<LeaseAgreement> LeaseAgreements { get; set; }
        public DbSet<LeaseExtentionRequest> LeaseExtentionRequests { get; set; }
        public DbSet<PaymentInstallment> PaymentInstallments { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<ReservationInquiry> ReservationInquiries { get; set; }
        public DbSet<WaitingApplication> WaitingApplications { get; set; }



        //For Many-Many relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Enum conversions
            modelBuilder.Entity<Apartment>()
                .Property(a => a.Status)
                .HasConversion(
                    x => x.ToString(),
                    x => (ApartmentAvailabilityStatus)Enum.Parse(typeof(ApartmentAvailabilityStatus), x));
            
            modelBuilder.Entity<ParkingSpace>()
                .Property(a => a.Status)
                .HasConversion(
                    x => x.ToString(),
                    x => (ParkingSpaceStatus)Enum.Parse(typeof(ParkingSpaceStatus), x));

            modelBuilder.Entity<UserModel>()
                .Property(a => a.Role)
                .HasConversion(
                    x => x.ToString(),
                    x => (UserRole)Enum.Parse(typeof(UserRole), x));

            modelBuilder.Entity<LeaseAgreement>()
                .Property(a => a.Status)
                .HasConversion(
                    x => x.ToString(),
                    x => (LeaseAgreementStatus)Enum.Parse(typeof(LeaseAgreementStatus), x));

            modelBuilder.Entity<Dependant>()
                .Property(a => a.Relationship)
                .HasConversion(
                    x => x.ToString(),
                    x => (DependantRelationshipType)Enum.Parse(typeof(DependantRelationshipType), x));

            modelBuilder.Entity<LeaseExtentionRequest>()
                .Property(a => a.Status)
                .HasConversion(
                    x => x.ToString(),
                    x => (LeaseExtentionRequestStatus)Enum.Parse(typeof(LeaseExtentionRequestStatus), x));

            modelBuilder.Entity<PaymentInstallment>()
                .Property(a => a.Status)
                .HasConversion(
                    x => x.ToString(),
                    x => (PaymentInstallmentStatus)Enum.Parse(typeof(PaymentInstallmentStatus), x));
            
            modelBuilder.Entity<Payment>()
                .Property(a => a.PaymentReason)
                .HasConversion(
                    x => x.ToString(),
                    x => (PaymentReason)Enum.Parse(typeof(PaymentReason), x));

            modelBuilder.Entity<ReservationInquiry>()
                .Property(a => a.Status)
                .HasConversion(
                    x => x.ToString(),
                    x => (InquiryStatus)Enum.Parse(typeof(InquiryStatus), x));
            #endregion

            modelBuilder.Entity<ApartmentClassFacility>()
                .HasKey(i => new { i.Id, i.ApartmentClassId });

            modelBuilder.Entity<ApartmentClassFacility>()
                .HasOne(ac => ac.ApartmentClass)
                .WithMany(a => a.Facilities)
                .HasForeignKey(ac => ac.ApartmentClassId);

            modelBuilder.Entity<Apartment>()
                .HasKey(i => new { i.Id });

            modelBuilder.Entity<Apartment>()
                .HasOne(ac => ac.Building)
                .WithMany(b => b.Apartments)
                .HasForeignKey(ac => ac.BuildingId);

            modelBuilder.Entity<Apartment>()
                .HasOne(ac => ac.ApartmentClass)
                .WithMany(b => b.Apartments)
                .HasForeignKey(ac => ac.ApartmentClassId);

            modelBuilder.Entity<Dependant>()
                .HasKey(i => new { i.Id });

            modelBuilder.Entity<Dependant>()
                .HasOne(d => d.ChiefOccupant)
                .WithMany(c => c.Dependants)
                .HasForeignKey(d => d.ChiefOccupantId);

            modelBuilder.Entity<LeaseExtentionRequest>()
                .HasKey(i => new { i.Id });

            modelBuilder.Entity<LeaseExtentionRequest>()
                .HasOne(d => d.LeaseAgreement)
                .WithMany(c => c.LeaseExtentionRequests)
                .HasForeignKey(d => d.LeaseAgreementId);
            
            modelBuilder.Entity<PaymentInstallment>()
                .HasKey(i => new { i.Id });

            modelBuilder.Entity<PaymentInstallment>()
                .HasOne(d => d.LeaseAgreement)
                .WithMany(c => c.PaymentInstallments)
                .HasForeignKey(d => d.LeaseAgreementId);

            modelBuilder.Entity<Payment>()
                .HasKey(i => new { i.Id });

            modelBuilder.Entity<Payment>()
                .HasOne(d => d.LeaseAgreement)
                .WithMany(c => c.DonePayments)
                .HasForeignKey(d => d.LeaseAgreementId);
        }
    }
}

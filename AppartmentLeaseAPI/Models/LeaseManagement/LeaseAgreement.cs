﻿using AppartmentLeaseAPI.Data.Enums;
using AppartmentLeaseAPI.Models.Apartments;
using AppartmentLeaseAPI.Models.Payments;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppartmentLeaseAPI.Models.LeaseManagement
{
    public class LeaseAgreement
    {
        [Key]
        public int Id { get; set; }

        public LeaseAgreementStatus Status { get; set; }

        public DateTime StartDate;

        public DateTime EndtDate;

        public bool IsRefundableDepositPaid;

        public bool IsMonthAdvancePaid;

        public double TotalCost;

        [ForeignKey("PurchasedParkingSpaceId")]
        public int? PurchasedParkingSpaceId { get; set; }
        public ParkingSpace? PurchasedParkingSpace { get; set; }

        [ForeignKey("ApartmentId")]
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        public ICollection<LeaseExtentionRequest>? LeaseExtentionRequests { get; set; }

        public ICollection<PaymentInstallment> PaymentInstallments { get; set; }

        public ICollection<Payment> DonePayments { get; set; }
    }
}

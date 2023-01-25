﻿using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Models.Apartments;

namespace AppartmentLeaseAPI.Interfaces
{
    public interface IApartmentManagementRepository
    {
        ICollection<ApartmentClassFacilitiesDto> GetApartmentClasses();
    }
}
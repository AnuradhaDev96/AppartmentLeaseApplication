using AppartmentLeaseAPI.Data;
using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Interfaces;
using AppartmentLeaseAPI.Models.Apartments;
using AutoMapper;

namespace AppartmentLeaseAPI.Repositories
{
    public class ApartmentManagementRepository : IApartmentManagementRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ApartmentManagementRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<ApartmentClassFacilitiesDto> GetApartmentClasses()
        {
            var apartmentClasses = _context.ApartmentClasses.ToList();
            var mappedResult = _mapper.Map<List<ApartmentClassFacilitiesDto>>(apartmentClasses);

            foreach (var apartmentClass in mappedResult)
            {
                var facilites = _context.ApartmentClassFacilities.Where(x => x.ApartmentClassId == apartmentClass.Id).ToList();
                List<string>? facilityNames = new List<string>();
                foreach (var facility in facilites)
                {
                    facilityNames.Add(facility.Name);
                }
                apartmentClass.AvailableFacilites = facilityNames;
            }
            return mappedResult;
        }
    }
}

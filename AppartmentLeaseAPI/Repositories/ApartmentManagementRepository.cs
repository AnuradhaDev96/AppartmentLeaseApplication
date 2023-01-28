using AppartmentLeaseAPI.Data;
using AppartmentLeaseAPI.Data.Enums;
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

        public ICollection<ApartmentGetDto> FilterAvailableApartments(string location = "", string apartmentType = "")
        {
            var availableApartments = _context.Apartments.Where(a => a.Status == ApartmentAvailabilityStatus.Available || a.Status == ApartmentAvailabilityStatus.Maintenance).ToList();

            // Get related data
            foreach(var apartment in availableApartments)
            {
                // Get Building details
                var builingBelongs = _context.Buildings.Where(a => a.Id == apartment.BuildingId).FirstOrDefault();

                if (builingBelongs != null)
                    apartment.Building = builingBelongs;

                // Get Parking details
                var assignedParkingSpace = _context.ParkingSpaces.Where(a => a.Id == apartment.ParkingSpaceId).FirstOrDefault();

                if (assignedParkingSpace != null)
                    apartment.ParkingSpace = assignedParkingSpace;

                // Get class details
                var classOfApartment = _context.ApartmentClasses.Where(a => a.Id == apartment.ApartmentClassId).FirstOrDefault();

                if (classOfApartment != null)
                    apartment.ApartmentClass = classOfApartment;
            }

            var query = availableApartments.AsQueryable();

            // Filter
            if (!string.IsNullOrEmpty(location))
                query = query.Where(a => a.Building.Location.Trim().Replace(" ", "").ToLower() == location.Trim().Replace(" ", "").ToLower());
            if (!string.IsNullOrEmpty(apartmentType))
                query = query.Where(a => a.ApartmentClass.Name.Trim().Replace(" ", "").ToLower() == apartmentType.Trim().Replace(" ", "").ToLower());

            // Map to result
            List<ApartmentGetDto> mappedApartments = new List<ApartmentGetDto>();

            //var list = query.ToList();

            foreach (var apartment in query.ToList())
            {
                var mappedItem = _mapper.Map<ApartmentGetDto>(apartment);

                mappedItem.BuildingName = apartment.Building.Name;
                mappedItem.BuildingLocation = apartment.Building.Location;
                mappedItem.ReservedParkingSpace = apartment.ParkingSpace.LotNo;
                mappedItem.ApartmentClassName = apartment.ApartmentClass.Name;

                mappedApartments.Add(mappedItem);
            }

            return mappedApartments;

        }

        public ApartmentClass? GetApartmentClassDetails(int apartmentId)
        {
            var apartment = _context.Apartments.Where(ap => ap.Id == apartmentId).FirstOrDefault();

            return _context.ApartmentClasses.Where(a => a.Id == apartment.ApartmentClassId).FirstOrDefault();
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

        public ICollection<ApartmentGetDto> GetAvailableApartments()
        {
            var availableApartments = _context.Apartments.Where(a => a.Status == ApartmentAvailabilityStatus.Available || a.Status == ApartmentAvailabilityStatus.Maintenance).ToList();

            //var mappedApartments = _mapper.Map<List<ApartmentGetDto>>(availableApartments);
            List<ApartmentGetDto> mappedApartments = new List<ApartmentGetDto>();

            foreach (var apartment in availableApartments)
            {
                // Get Building details
                var builingBelongs = _context.Buildings.Where(a => a.Id == apartment.BuildingId).FirstOrDefault();

                if (builingBelongs != null)
                    apartment.Building = builingBelongs;

                // Get Parking details
                var assignedParkingSpace = _context.ParkingSpaces.Where(a => a.Id == apartment.ParkingSpaceId).FirstOrDefault();

                if (assignedParkingSpace != null)
                    apartment.ParkingSpace = assignedParkingSpace;

                // Get class details
                var classOfApartment = _context.ApartmentClasses.Where(a => a.Id == apartment.ApartmentClassId).FirstOrDefault();

                if (classOfApartment != null)
                    apartment.ApartmentClass = classOfApartment;

                var mappedItem = _mapper.Map<ApartmentGetDto>(apartment);

                mappedItem.BuildingName = apartment.Building.Name;
                mappedItem.BuildingLocation = apartment.Building.Location;
                mappedItem.ReservedParkingSpace = apartment.ParkingSpace.LotNo;                
                mappedItem.ApartmentClassName = apartment.ApartmentClass.Name;

                mappedApartments.Add(mappedItem);

            }

            return mappedApartments;
        }

        public ICollection<ParkingSpace> GetAvailableParkingSpaces()
        {
            return _context.ParkingSpaces.Where(p => p.Status == ParkingSpaceStatus.Available).ToList();
        }

        public bool UpdateApartmentStatus(int apartmentId, ApartmentAvailabilityStatus statusToUpdate)
        {
            var apartmentToUpdate = _context.Apartments.Where(a => a.Id == apartmentId).FirstOrDefault();

            if (apartmentToUpdate == null)
                return false;

            apartmentToUpdate.Status = statusToUpdate;

            _context.Apartments.Update(apartmentToUpdate);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateParkingSpaceStatus(int parkingSpaceId, ParkingSpaceStatus statusToUpdate)
        {
            var parkingSpaceToUpdate = _context.ParkingSpaces.Where(p => p.Id == parkingSpaceId).FirstOrDefault();

            if (parkingSpaceToUpdate == null)
                return false;

            parkingSpaceToUpdate.Status = statusToUpdate;

            _context.ParkingSpaces.Update(parkingSpaceToUpdate);

            return Save();
        }

        public ParkingSpace? GetParkingSpaceById(int parkingSpaceId)
        {
            var parkingSpace = _context.ParkingSpaces.Where(ap => ap.Id == parkingSpaceId).FirstOrDefault();

            return parkingSpace;
        }
    }
}

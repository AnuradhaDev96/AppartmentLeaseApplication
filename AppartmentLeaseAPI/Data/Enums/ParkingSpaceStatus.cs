namespace AppartmentLeaseAPI.Data.Enums
{
    public enum ParkingSpaceStatus
    {
        /// <summary>
        /// These are reserved for apartments
        /// </summary>
        Reserved,

        /// <summary>
        /// Available for purchase in Lease Agreement
        /// </summary>
        Available,

        /// <summary>
        /// These are purchased additionally for Lease Agreement
        /// </summary>
        Purchased
    }
}

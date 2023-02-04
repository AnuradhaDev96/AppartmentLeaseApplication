namespace AppartmentLeaseAPI.Data.Enums
{
    public enum LeaseAgreementStatus
    {
        New,
        Started,
        Extended,
        /// <summary>
        /// Fresh state is assigned for fresh lease agreement extended from parent lease agreement
        /// </summary>
        Fresh,
        Ended
    }
}

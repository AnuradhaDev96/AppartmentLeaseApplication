using AppartmentLeaseApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Interfaces
{
    public interface IUserManagementEndpoint
    {
        Task<List<SystemUser>> GetAllUsers();
    }
}
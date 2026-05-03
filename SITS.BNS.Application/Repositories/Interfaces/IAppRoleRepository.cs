using SITS.BNS.Entities.Entity;
using System.Data;

namespace SITS.BNS.Application.Repositories.Interfaces
{
    public interface IAppRoleRepository
    {
        Task<AppRole?> GetRoleById(int id);
    }
}

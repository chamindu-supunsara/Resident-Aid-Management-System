using SITS.BNS.Entities.Entity;

namespace SITS.BNS.Application.Repositories.Interfaces
{
    public interface IAppUserRoleRepository
    {
        Task<List<AppUserRole>> GetAllUserRoleByUserId(int userid);
    }
}

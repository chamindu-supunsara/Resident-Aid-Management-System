using Microsoft.EntityFrameworkCore;
using SITS.BNS.Application.Repositories.Common;
using SITS.BNS.Application.Repositories.Interfaces;
using SITS.BNS.Entities.Entity;
using SITS.BNS.Infrastructure;

namespace SITS.BNS.Application.Repositories
{
    public class AppUserRoleRepository(DbContext context) : Repository<AppUserRole>(context), IAppUserRoleRepository
    {
        public async Task<List<AppUserRole>> GetAllUserRoleByUserId(int userid)
        {
            var UserRoles = await FindAsync(x => x.IsActive && x.AppUserId == userid);

            if (UserRoles.Count() == 0)
            {
                return new List<AppUserRole>();
            }
            else
            {
                return UserRoles.ToList();
            }
        }
    }
}

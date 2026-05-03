using Microsoft.EntityFrameworkCore;
using SITS.BNS.Application.Repositories.Common;
using SITS.BNS.Application.Repositories.Interfaces;
using SITS.BNS.Entities.Entity;
using SITS.BNS.Infrastructure;
using System.Data;

namespace SITS.BNS.Application.Repositories
{
    public class AppRoleRepository(DbContext context) : Repository<AppRole>(context), IAppRoleRepository
    {
        public async Task<AppRole?> GetRoleById(int id)
        {
            return await GetSingleOrDefaultAsync(x => x.IsActive && x.ID == id);
        }
    }
}

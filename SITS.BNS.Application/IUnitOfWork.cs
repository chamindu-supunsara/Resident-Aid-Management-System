using SITS.BNS.Application.Repositories.Interfaces;
using SITS.BNS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUsers { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IAppUserRoleRepository UserRoles { get; }
        IAppRoleRepository Roles { get; }
        ILeadsRepository Leads { get; }
        IGramaOfficeRepository GramaOffice { get; }
        IAidDetailsRepository AidDetails { get; }
        IFamilyRepository Family { get; }
        IFamilyMembersRepository FamilyMembers { get; }
        ApplicationDbContext Context { get; }

        Task<int> SaveChangesAsync();
    }
}

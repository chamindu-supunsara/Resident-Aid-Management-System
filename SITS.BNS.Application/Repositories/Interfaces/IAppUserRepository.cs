using SITS.BNS.Application.DTO.AppUsers;
using SITS.BNS.Application.DTO.AuditLog;
using SITS.BNS.Application.DTO.Company;
using SITS.BNS.Application.DTO.Family;
using SITS.BNS.Application.DTO.GramaOffice;
using SITS.BNS.Application.DTO.Leads;
using SITS.BNS.Entities.AuthModels;
using SITS.BNS.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.Repositories.Interfaces
{
    public interface IAppUserRepository
    {
        Task<int> SaveAppUser(AppUser appUser);
        Task<int> RegisterUsers(AppUserRegisterDto req);
        Task<bool> CheckEmailExists(AppUserCheckEmailDto appUserCheckEmailDto);
        Task<int> UpdatePassword(AppUserUpdatePassword req);
        Task<int> UpdateAppUsers(AppUserUpdateDto appUser);
        Task<int> DeleteAppUser(int id);
        Task<AuthResponse> Authenticate(AuthRequest authRequest);
        IEnumerable<ViewOfficersDto> GetAllOfficers(LeadsViewDto LeadsViewDto);
        IEnumerable<AllAuditDto> GetAllAuditLogs();

    }
}

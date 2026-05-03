using SITS.BNS.Application.DTO.AppUsers;
using SITS.BNS.Application.DTO.Family;
using SITS.BNS.Application.DTO.Leads;
using SITS.BNS.Application.DTO.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.Repositories.Interfaces
{
    public interface IFamilyRepository
    {
        Task<int> SaveLeadForm(LeadSaveDto req);
        Task<int> UpdateHouse(UpdateHouseDto req);
        Task<int> DeleteHouse(DeleteHouseDto req);
        Task<ViewFamilyDataDto> GetHousebyID(int id);
        IEnumerable<ViewMemberDto> GetAllMembers(LeadsViewDto LeadsViewDto);
    }
}

using SITS.BNS.Application.DTO.Family;
using SITS.BNS.Application.DTO.Members;
using SITS.BNS.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.Repositories.Interfaces
{
    public interface IFamilyMembersRepository
    {
        Task<ViewMemberDataDto> GetMemberbyID(int id);
        Task<int> UpdateMember(UpdateMemberDto req);
        Task<int> DeleteMember(DeleteMemberDto req);
    }
}

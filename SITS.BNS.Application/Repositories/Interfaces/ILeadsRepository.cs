using SITS.BNS.Application.DTO.Company;
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
    public interface ILeadsRepository
    {
        IEnumerable<LeadsDashboardStatus> GetDashboardStatus(LeadsViewDto LeadsViewDto);
        IEnumerable<LeadViewByMonth> GetDashboardPie(LeadsViewDto LeadsViewDto);
        IEnumerable<LeadViewByMonth> GetDashboardBar(LeadsViewDto LeadsViewDto);
    }
}

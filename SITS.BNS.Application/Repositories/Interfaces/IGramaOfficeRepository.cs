using SITS.BNS.Application.DTO.Company;
using SITS.BNS.Application.DTO.Family;
using SITS.BNS.Application.DTO.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.Repositories.Interfaces
{
    public interface IGramaOfficeRepository
    {
        IEnumerable<CompanyListAvailableDto> GetAllCompanyIsAvailable();
    }
}

using SITS.BNS.Application.DTO.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.AID
{
    public class ViewAidsDto
    {
        public int ID { get; set; }
        public string? FullName { get; set; }
        public string? Nic { get; set; }
        public string? FamilyNo { get; set; }
        public string? HouseUnitNo { get; set; }
        public string? SubNo { get; set; }
        public string? HouseholdNo { get; set; }
        public string? GramaCode { get; set; }
        public int Age { get; set; }
        public List<string>? AidsList { get; set; }

    }

}

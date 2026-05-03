using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.Family
{
    public class ViewMemberDto
    {
        public int ID { get; set; }
        public string? FamilyNo { get; set; }
        public string? HouseUnitNo { get; set; }
        public string? SubNo { get; set; }
        public string? HouseholdNo { get; set; }
        public string? Wasama { get; set; }
        public string? WasamNo { get; set; }
        public string? OfficerName { get; set; }
    }
}

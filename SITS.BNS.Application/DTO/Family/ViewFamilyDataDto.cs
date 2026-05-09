using SITS.BNS.Application.DTO.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.Family
{
    public class ViewFamilyDataDto
    {
        public int ID { get; set; }
        public string? FamilyNo { get; set; }
        public string? HouseUnitNo { get; set; }
        public string? SubNo { get; set; }
        public string? HouseholdNo { get; set; }
        public string? Wasama { get; set; }
        public string? WasamNo { get; set; }
        public string? OfficerName { get; set; }
        public List<FMembersDto>? FamilyMembers { get; set; }
    }

    public class FMembersDto
    {
        public int id { get; set; }
        public string? FullName { get; set; }
        public string? Nic { get; set; }
        public DateTime? Birthday { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
        public string? Job { get; set; }
        public string? Mobile { get; set; }
        public decimal? Income { get; set; }
        public List<string>? AidsList { get; set; }
        /// <summary>True when stored like a child record (no job / marital status).</summary>
        public bool IsChild { get; set; }
    }
}

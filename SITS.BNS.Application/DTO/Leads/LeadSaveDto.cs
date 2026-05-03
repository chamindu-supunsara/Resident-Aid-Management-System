using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.Leads
{
    public class LeadSaveDto
    {
        public int ID { get; set; }
        public string? GramaOfficeCode { get; set; }
        public string? OfficerId { get; set; }
        public string? FamilyNumber { get; set; }
        public string? HouseUnitNumber { get; set; }
        public string? SubNumber { get; set; }
        public string? HouseholdNo { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Mobile { get; set; }
        public string? Job { get; set; }
        public string? Income { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Nic { get; set; }
        public List<AidSelectionsDto>? AidSelections { get; set; }
        public List<FamilyMemberDto>? FamilyMember { get; set; }
        public List<FamilyChildDto>? FamilyChild { get; set; }
    }

    public class FamilyMemberDto
    {
        public string? FullName { get; set; }
        public string? Nic { get; set; }
        public DateTime? Birthday { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
        public string? Job { get; set; }
        public string? Mobile { get; set; }
        public string? Income { get; set; }
        public List<AidSelectionsDto>? AidSelections { get; set; }
    }

    public class FamilyChildDto
    {
        public string? FullName { get; set; }
        public string? Nic { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }
        public List<AidSelectionsDto>? AidSelections { get; set; }
    }

    public class AidSelectionsDto
    {
        public bool Special { get; set; }
        public bool Kidney { get; set; }
        public bool Health { get; set; }
        public bool Scholarship { get; set; }
        public bool Disability { get; set; }
        public bool Aswesuma_1 { get; set; }
        public bool Aswesuma_2 { get; set; }
        public bool Aswesuma_3 { get; set; }
        public bool Aswesuma_4 { get; set; }
        public bool Aswesuma_5 { get; set; }
        public bool Aswesuma_6 { get; set; }
        public bool Elders { get; set; }
        public bool Others { get; set; }
        public string? OthersDetails { get; set; }
    }
}

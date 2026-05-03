using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.Members
{
    public class ViewMemberDataDto
    {
        public int? ID { get; set; }
        public string? FullName { get; set; }
        public string? NIC { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Job { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? Income { get; set; }
        public int? FamilyID { get; set; }

        public bool? SpecialAids { get; set; }
        public bool? Other { get; set; }
        public bool? Aswesuma_1 { get; set; }
        public bool? Aswesuma_2 { get; set; }
        public bool? Aswesuma_3 { get; set; }
        public bool? Aswesuma_4 { get; set; }
        public bool? Aswesuma_5 { get; set; }
        public bool? Aswesuma_6 { get; set; }
        public bool? Kidney { get; set; }
        public bool? Scholarship { get; set; }
        public bool? Elders { get; set; }
        public bool? Health { get; set; }
        public bool? Disability { get; set; }

        public string? Others { get; set; }
    }
}

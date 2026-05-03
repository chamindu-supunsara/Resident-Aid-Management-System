using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.Members
{
    public class UpdateMemberDto
    {
        public int ID { get; set; }
        public string? FullName { get; set; }
        public string? Nic { get; set; }
        public DateTime? Birthday { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
        public string? Job { get; set; }
        public string? Mobile { get; set; }
        public string? Income { get; set; }
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

using SITS.BNS.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.Entity
{
    public class AidDetail : CommonEntity
    {
        public bool SpecialAids { get; set; }
        public bool OtherAids { get; set; }
        public bool Aswesuma_1 { get; set; }
        public bool Aswesuma_2 { get; set; }
        public bool Aswesuma_3 { get; set; }
        public bool Aswesuma_4 { get; set; }
        public bool Aswesuma_5 { get; set; }
        public bool Aswesuma_6 { get; set; }
        public bool KidneyAid { get; set; }
        public bool Scholarship { get; set; }
        public bool EldersAid { get; set; }
        public bool HealthAid { get; set; }
        public bool DisabilityAid { get; set; }
        public string? Other { get; set; }

        [ForeignKey("MemberId")]
        public int MemberId { get; set; }
        public virtual FamilyMember? FamilyMember { get; set; }
    }

}

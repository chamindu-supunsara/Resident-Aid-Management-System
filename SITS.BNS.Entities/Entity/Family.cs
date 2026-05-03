using SITS.BNS.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.Entity
{
    public class Family : CommonEntity
    {
        public string? FamilyNo { get; set; }
        public string? HouseUnitNo { get; set; }
        public string? SubNo { get; set; }
        public string? HouseholdNo { get; set; }

        [ForeignKey("GramaOfficeId")]
        public int GramaOfficeId { get; set; }
        public GramaOffice GramaOffice { get; set; } = null!;

        [ForeignKey("OfficerId")]
        public int OfficerId { get; set; }
        public AppUser Officer { get; set; } = null!;

        public ICollection<FamilyMember> Members { get; set; } = [];
    }


}

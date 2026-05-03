using SITS.BNS.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.Entity
{
    public class FamilyMember : CommonEntity
    {
        public string? FullName { get; set; }
        public string? NIC { get; set; }
        public DateTime BirthDay { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Job { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Income { get; set; }

        [ForeignKey("FamilyId")]
        public int FamilyId { get; set; }
        public virtual Family? Family { get; set; } 

        public virtual AidDetail? AidDetail { get; set; }
    }

}

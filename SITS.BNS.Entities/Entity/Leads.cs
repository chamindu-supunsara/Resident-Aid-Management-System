using SITS.BNS.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.Entity
{
    public class Leads : CommonEntity
    {
        public string? RefNo { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Nic { get; set; }
        public string? Address { get; set; }
        public int? Submitby { get; set; }

        [ForeignKey("Submitby")]
        public virtual AppUser? AppUser { get; set; }
    }
}

using SITS.BNS.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.Entity
{
    public class AuditLog : CommonEntity
    {
        public string? Category { get; set; }
        public string? Action { get; set; }
        public string? EditBy { get; set; }

        [ForeignKey("AppUserID")]
        public int? AppUserID { get; set; }
        public AppUser? AppUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.AuditLog
{
    public class AllAuditDto
    {
        public int ID { get; set; }
        public string? Category { get; set; }
        public string? Action { get; set; }
        public string? EditedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UserId { get; set; }
    }
}

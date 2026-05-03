using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.AuditLog
{
    public class AuditLogDto
    {
        public int AppUserId { get; set; }
        public string? Category { get; set; }
        public string? Action { get; set; }
        public string? EditedBy { get; set; }
    }
}

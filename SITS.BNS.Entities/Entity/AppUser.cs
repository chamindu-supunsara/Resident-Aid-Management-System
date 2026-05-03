using SITS.BNS.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.Entity
{
    public class AppUser : CommonEntity
    {
        public string? UserEmail { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Organization { get; set; }
        public int OrganizationId { get; set; }
        public string? Mobile { get; set; }

        [ForeignKey("GramaOfficeId")]
        public int? GramaOfficeId { get; set; } 
        public GramaOffice? GramaOffice { get; set; }

        public ICollection<AppUserRole>? AppUserRoles { get; set; }
        public ICollection<Family>? Families { get; set; }
    }

}

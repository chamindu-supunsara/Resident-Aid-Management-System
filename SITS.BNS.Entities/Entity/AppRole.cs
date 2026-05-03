using SITS.BNS.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.Entity
{
    public class AppRole : CommonEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<AppUserRole>? AppUserRoles { get; set; }
    }

}

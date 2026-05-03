using SITS.BNS.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.Entity
{
    public class GramaOffice : CommonEntity
    {
        public string? Name { get; set; }
        public string? LocationCode { get; set; }
        public bool? IsAvailable { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }

        public ICollection<AppUser> AppUsers { get; set; } = [];
        public ICollection<Family> Families { get; set; } = [];
    }

}

using SITS.BNS.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.RequestResponseModels
{
    public class AppUserAuthenticateResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public AppUser? User { get; set; }
    }
}

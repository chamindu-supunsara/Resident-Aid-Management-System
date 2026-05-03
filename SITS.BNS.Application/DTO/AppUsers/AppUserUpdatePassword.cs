using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.AppUsers
{
    public class AppUserUpdatePassword
    {
        public string? UserEmail { get; set; }
        public string? NewPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.AppUsers
{
    public class AppUserRegisterDto
    {
        public int ID { get; set; }
        public int GramaID { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? CompanyName { get; set; }
        public int CompanyID { get; set; }
        public string? Password { get; set; }

    }
}

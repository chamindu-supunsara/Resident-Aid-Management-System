using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.GramaOffice
{
    public class ViewOfficersDto
    {
        public int? ID { get; set; }
        public string? Firstname { get; set; }
        public string? UserEmail { get; set; }
        public string? Lastname { get; set; }
        public string? WasamName { get; set; }
        public string? Mobile { get; set; }
        public bool? Status { get; set; }
    }
}

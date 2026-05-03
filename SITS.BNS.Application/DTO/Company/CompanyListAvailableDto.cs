using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.DTO.Company
{
    public class CompanyListAvailableDto
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? LocationCode { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public bool? IsAvailable { get; set; }
    }
}

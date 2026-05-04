using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SITS.BNS.Application.DTO.Company;
using SITS.BNS.Application.Repositories.Common;
using SITS.BNS.Application.Repositories.Interfaces;
using SITS.BNS.Entities.Common;
using SITS.BNS.Entities.Entity;
using SITS.BNS.Entities.Interfaces;
using SITS.BNS.Infrastructure;

namespace SITS.BNS.Application.Repositories
{
    public class GramaOfficeRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger) : Repository<GramaOffice>(context), IGramaOfficeRepository
    {
        private readonly ILogger<UnitOfWork> _logger = logger;
        private new readonly ApplicationDbContext _context = context;

        public IEnumerable<CompanyListAvailableDto> GetAllCompanyIsAvailable()
        {
            try
            {
                var Company = this.Find(c => c.IsActive && c.IsAvailable == true).ToList();

                var companyDtos = Company.Select(company => new CompanyListAvailableDto
                {
                    ID = company.ID,
                    Name = company.Name,
                    LocationCode = company.LocationCode,
                    Longitude = company.Longitude,
                    Latitude = company.Latitude,
                    IsAvailable = company.IsAvailable
                })
                .ToList();

                return companyDtos;

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: GetCompanies&Products " + e);
                throw;
            }

        }
    }
}

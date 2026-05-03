using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SITS.BNS.Application.DTO.Leads;
using SITS.BNS.Application.Repositories.Common;
using SITS.BNS.Application.Repositories.Interfaces;
using SITS.BNS.Entities.Entity;
using SITS.BNS.Infrastructure;

namespace SITS.BNS.Application.Repositories
{
    public class LeadsRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger) : Repository<Leads>(context), ILeadsRepository
    {
        private readonly ILogger<UnitOfWork> _logger = logger;
        private new readonly ApplicationDbContext _context = context;

        public IEnumerable<LeadsDashboardStatus> GetDashboardStatus(LeadsViewDto req)
        {
            try
            {
                if (req.UserRole == "Admin")
                {
                    var familyCount = _context.Family.Where(x => x.IsActive).Count();
                    var peopleCount = _context.FamilyMember.Where(x => x.IsActive).Count();
                    var aidsCount = _context.AidDetail.Where(x => x.IsActive &&
                            (x.SpecialAids || x.Aswesuma_1 || x.Aswesuma_2 || x.Aswesuma_3 ||
                             x.Aswesuma_4 || x.Aswesuma_5 || x.Aswesuma_6 || x.KidneyAid ||
                             x.Scholarship || x.EldersAid || x.HealthAid || x.DisabilityAid || x.OtherAids)).Count();

                    if (familyCount == 0 && peopleCount == 0 && aidsCount == 0)
                    {
                        return [];
                    }

                    return
                    [
                        new LeadsDashboardStatus { Data = [familyCount, peopleCount, aidsCount] }
                    ];

                }
                else if (req.UserRole == "User")
                {
                    var families = _context.Family.Where(x => x.IsActive && x.OfficerId == req.UserId).ToList();
                    var familyIds = families.Select(f => f.ID).ToList();

                    var familyMembers = _context.FamilyMember.Where(x => x.IsActive && familyIds.Contains(x.FamilyId)).ToList();
                    var familyMembersIds = familyMembers.Select(x => x.ID).ToList();

                    var aids = _context.AidDetail.Where(x => x.IsActive && familyMembersIds.Contains(x.MemberId) &&
                            (x.SpecialAids || x.Aswesuma_1 || x.Aswesuma_2 || x.Aswesuma_3 || x.Aswesuma_4 || x.Aswesuma_5 || x.Aswesuma_6 || x.KidneyAid || x.Scholarship || x.EldersAid || x.HealthAid || x.DisabilityAid || x.OtherAids)).ToList();
                    var aidIds = aids.Select(x => x.ID).ToList();

                    var familyCount = families.Count;
                    var peopleCount = familyMembers.Count;
                    var aidsCount = aids.Count;

                    if (familyCount == 0 && peopleCount == 0 && aidsCount == 0)
                    {
                        return [];
                    }

                    return
                    [
                        new LeadsDashboardStatus { Data = [familyCount, peopleCount, aidsCount] }
                    ];
                }

                return [];

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: GetDashboardStatus  " + e);
                throw;
            }
        }

        public IEnumerable<LeadViewByMonth> GetDashboardPie(LeadsViewDto req)
        {
            try
            {
                var labels = new[]
                {
                    "Special", "Aswesuma Rs 15000", "Aswesuma Rs 10000", "Aswesuma Vulnerable Rs 5000",
                    "Aswesuma Transient Rs 5000", "Aswesuma Appeal", "Aswesuma Cycle 2",
                    "Kidney", "Scholarship", "Elders", "Health", "Disability", "Other"
                };

                IQueryable<AidDetail> aidQuery;

                if (req.UserRole == "Admin")
                {
                    aidQuery = _context.AidDetail.Where(x => x.IsActive);
                }
                else if (req.UserRole == "User")
                {
                    var familyIds = _context.Family
                        .Where(x => x.IsActive && x.OfficerId == req.UserId)
                        .Select(f => f.ID);

                    var memberIds = _context.FamilyMember
                        .Where(x => x.IsActive && familyIds.Contains(x.FamilyId))
                        .Select(m => m.ID);

                    aidQuery = _context.AidDetail
                        .Where(x => x.IsActive && memberIds.Contains(x.MemberId));
                }
                else
                {
                    return [];
                }

                var data = new[]
                {
                    aidQuery.Count(x => x.SpecialAids),
                    aidQuery.Count(x => x.Aswesuma_1),
                    aidQuery.Count(x => x.Aswesuma_2),
                    aidQuery.Count(x => x.Aswesuma_3),
                    aidQuery.Count(x => x.Aswesuma_4),
                    aidQuery.Count(x => x.Aswesuma_5),
                    aidQuery.Count(x => x.Aswesuma_6),
                    aidQuery.Count(x => x.KidneyAid),
                    aidQuery.Count(x => x.Scholarship),
                    aidQuery.Count(x => x.EldersAid),
                    aidQuery.Count(x => x.HealthAid),
                    aidQuery.Count(x => x.DisabilityAid),
                    aidQuery.Count(x => x.OtherAids)
                };

                if (data.All(count => count == 0))
                {
                    return [];
                }

                var result = new LeadViewByMonth
                {
                    Labels = labels,
                    Data = data
                };

                return [result];
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: GetDashboardPieChart  " + e);
                throw;
            }
        }

        public IEnumerable<LeadViewByMonth> GetDashboardBar(LeadsViewDto req)
        {
            try
            {
                if (req.UserId != 0)
                {
                    var data = (
                        from go in _context.GramaOffice
                        join f in _context.Family on go.ID equals f.GramaOfficeId
                        join m in _context.FamilyMember on f.ID equals m.FamilyId
                        where go.IsActive && f.IsActive && m.IsActive
                        group m by new { go.ID, go.LocationCode } into g
                        select new
                        {
                            g.Key.ID,
                            g.Key.LocationCode,
                            Count = g.Count()
                        }
                    ).ToList();

                    var allOffices = _context.GramaOffice
                        .Where(go => go.IsActive)
                        .OrderBy(go => go.LocationCode)
                        .Select(go => new { go.ID, go.LocationCode })
                        .ToList();

                    var labels = allOffices.Select(g => g.LocationCode ?? string.Empty).ToArray();
                    var counts = allOffices.Select(g => data.FirstOrDefault(d => d.ID == g.ID)?.Count ?? 0).ToArray();

                    if (counts.All(c => c == 0))
                    {
                        return [];
                    }

                    var result = new LeadViewByMonth
                    {
                        Labels = labels,
                        Data = counts
                    };

                    return [result];
                }

                return [];
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: GetDashboardBarChart  " + e);
                throw;
            }
        }

    }
}

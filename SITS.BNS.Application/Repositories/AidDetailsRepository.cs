using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SITS.BNS.Application.DTO.AID;
using SITS.BNS.Application.DTO.Family;
using SITS.BNS.Application.DTO.Leads;
using SITS.BNS.Application.Repositories.Common;
using SITS.BNS.Application.Repositories.Interfaces;
using SITS.BNS.Entities.Common;
using SITS.BNS.Entities.Entity;
using SITS.BNS.Entities.Interfaces;
using SITS.BNS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.Repositories
{
    internal class AidDetailsRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger) : Repository<AidDetail>(context), IAidDetailsRepository
    {
        private readonly ILogger<UnitOfWork> _logger = logger;
        private new readonly ApplicationDbContext _context = context;

        public IEnumerable<ViewAidsDto> GetAllAids(LeadsViewDto req)
        {
            try
            {
                if (req.UserRole == "Admin")
                {
                    
                    var memberIds = _context.FamilyMember
                        .Where(m => m.IsActive && m.Family.IsActive)
                        .Select(m => new
                        {
                            m.ID,
                            m.FullName,
                            m.NIC,
                            m.BirthDay,
                            m.Family!.GramaOffice.LocationCode,
                            m.Family.FamilyNo,
                            m.Family.HouseUnitNo,
                            m.Family.SubNo,
                            m.Family.HouseholdNo
                        })
                        .ToList();

                    var aids = _context.AidDetail
                        .Where(a => memberIds.Select(m => m.ID).Contains(a.MemberId))
                        .ToList(); // also materialize to memory

                    var MembersList = memberIds.Select(l => new ViewAidsDto
                    {
                        ID = l.ID,
                        FamilyNo = l.FamilyNo,
                        HouseUnitNo = l.HouseUnitNo,
                        SubNo = l.SubNo,
                        HouseholdNo = l.HouseholdNo,
                        FullName = l.FullName,
                        GramaCode = l.LocationCode,
                        Age = CalculateAge(l.BirthDay) ?? 0,
                        Nic = l.NIC,
                        AidsList = aids
                            .Where(a => a.MemberId == l.ID)
                            .SelectMany(a =>
                            {
                                var aidTypes = new List<string>();
                                if (a.SpecialAids) aidTypes.Add("Special");
                                if (a.OtherAids) aidTypes.Add("Other");
                                if (a.Aswesuma_1) aidTypes.Add("Aswesuma Rs 15000");
                                if (a.Aswesuma_2) aidTypes.Add("Aswesuma Rs 10000");
                                if (a.Aswesuma_3) aidTypes.Add("Aswesuma Vulnerable Rs 5000");
                                if (a.Aswesuma_4) aidTypes.Add("Aswesuma Transirnt Rs 5000");
                                if (a.Aswesuma_5) aidTypes.Add("Aswesuma Appeal");
                                if (a.Aswesuma_6) aidTypes.Add("Aswesuma Cycle 2");
                                if (a.KidneyAid) aidTypes.Add("Kidney");
                                if (a.Scholarship) aidTypes.Add("Scholarship");
                                if (a.EldersAid) aidTypes.Add("Elders");
                                if (a.HealthAid) aidTypes.Add("Health");
                                if (a.DisabilityAid) aidTypes.Add("Disability");
                                return aidTypes;
                            })
                            .Distinct()
                            .ToList()

                    }).OrderByDescending(x => x.ID).ToList();

                    return MembersList;
                }
                else if (req.UserRole == "User")
                {
                    var memberIds = _context.FamilyMember
                        .Where(m => m.IsActive && m.Family.IsActive && m.Family.OfficerId == req.UserId)
                        .Select(m => new
                        {
                            m.ID,
                            m.FullName,
                            m.NIC,
                            m.BirthDay,
                            m.Family!.GramaOffice.LocationCode,
                            m.Family.FamilyNo,
                            m.Family.HouseUnitNo,
                            m.Family.SubNo,
                            m.Family.HouseholdNo
                        })
                        .ToList();

                    var aids = _context.AidDetail
                        .Where(a => memberIds.Select(m => m.ID).Contains(a.MemberId))
                        .ToList(); // also materialize to memory

                    var MembersList = memberIds.Select(l => new ViewAidsDto
                    {
                        ID = l.ID,
                        FamilyNo = l.FamilyNo,
                        HouseUnitNo = l.HouseUnitNo,
                        SubNo = l.SubNo,
                        GramaCode = l.LocationCode,
                        HouseholdNo = l.HouseholdNo,
                        FullName = l.FullName,
                        Nic = l.NIC,
                        Age = CalculateAge(l.BirthDay) ?? 0,
                        AidsList = aids
                            .Where(a => a.MemberId == l.ID)
                            .SelectMany(a =>
                            {
                                var aidTypes = new List<string>();
                                if (a.SpecialAids) aidTypes.Add("Special");
                                if (a.OtherAids) aidTypes.Add("Other");
                                if (a.Aswesuma_1) aidTypes.Add("Aswesuma Rs 15000");
                                if (a.Aswesuma_2) aidTypes.Add("Aswesuma Rs 10000");
                                if (a.Aswesuma_3) aidTypes.Add("Aswesuma Vulnerable Rs 5000");
                                if (a.Aswesuma_4) aidTypes.Add("Aswesuma Transirnt Rs 5000");
                                if (a.Aswesuma_5) aidTypes.Add("Aswesuma Appeal");
                                if (a.Aswesuma_6) aidTypes.Add("Aswesuma Cycle 2");
                                if (a.KidneyAid) aidTypes.Add("Kidney");
                                if (a.Scholarship) aidTypes.Add("Scholarship");
                                if (a.EldersAid) aidTypes.Add("Elders");
                                if (a.HealthAid) aidTypes.Add("Health");
                                if (a.DisabilityAid) aidTypes.Add("Disability");
                                return aidTypes;
                            })
                            .Distinct()
                            .ToList()

                    }).OrderByDescending(x => x.ID).ToList();

                    return MembersList;
                }
                else
                {
                    return [];
                }

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: Get Member List " + e);
                throw;
            }
        }

        public static int? CalculateAge(DateTime? birthDate)
        {
            if (birthDate == null) return null;

            var today = DateTime.Today;
            int age = today.Year - birthDate.Value.Year;

            if (birthDate.Value.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SITS.BNS.Application.DTO.Family;
using SITS.BNS.Application.DTO.Leads;
using SITS.BNS.Application.Repositories.Common;
using SITS.BNS.Application.Repositories.Interfaces;
using SITS.BNS.Entities.Entity;
using SITS.BNS.Entities.Interfaces;
using SITS.BNS.Infrastructure;

namespace SITS.BNS.Application.Repositories
{
    internal class FamilyRepository(ApplicationDbContext context, ICurrentUserService iCurrentUserService, ILogger<UnitOfWork> logger) : Repository<Family>(context), IFamilyRepository
    {
        private readonly ILogger<UnitOfWork> _logger = logger;
        private new readonly ApplicationDbContext _context = context;
        private readonly ICurrentUserService _ICurrentUserService = iCurrentUserService;

        public async Task<int> SaveLeadForm(LeadSaveDto req)
        {
            try
            {
                if (req.ID == 0)
                {
                    var user = await _context.AppUsers.FirstOrDefaultAsync(lp => lp.ID == Convert.ToInt32(req.OfficerId));

                    var families = new BNS.Entities.Entity.Family
                    {
                        FamilyNo = req.FamilyNumber,
                        HouseUnitNo = req.HouseUnitNumber,
                        SubNo = req.SubNumber,
                        HouseholdNo = req.HouseholdNo,
                        GramaOfficeId = Convert.ToInt32(user.GramaOfficeId),
                        OfficerId = user.ID,
                        IsActive = true,
                    };

                    _context.Family.Add(families);

                    await this.AuditLog("Add House", $"Village No: {families.FamilyNo}, House Hold No: {families.SubNo}", _ICurrentUserService.Name, int.Parse(_ICurrentUserService.UserId));
                    await this.SaveChangesAsync();

                    if (families.ID != 0)
                    {
                        var mainmember = new BNS.Entities.Entity.FamilyMember
                        {
                            FullName = req.FullName,
                            NIC = req.Nic,
                            BirthDay = Convert.ToDateTime(req.Birthday),
                            Gender = req.Gender,
                            MaritalStatus = req.MaritalStatus,
                            Job = req.Job,
                            PhoneNumber = req.Mobile,
                            FamilyId = families.ID,
                            Income = Convert.ToDecimal(req.Income),
                            IsActive = true,
                        };

                        _context.FamilyMember.Add(mainmember);
                        await this.SaveChangesAsync();

                        if (req.AidSelections != null)
                        {
                            foreach (var aid in req.AidSelections)
                            {
                                var aidDetail = new BNS.Entities.Entity.AidDetail
                                {
                                    MemberId = mainmember.ID,
                                    SpecialAids = aid.Special,
                                    Aswesuma_1 = aid.Aswesuma_1,
                                    Aswesuma_2 = aid.Aswesuma_2,
                                    Aswesuma_3 = aid.Aswesuma_3,
                                    Aswesuma_4 = aid.Aswesuma_4,
                                    Aswesuma_5 = aid.Aswesuma_5,
                                    Aswesuma_6 = aid.Aswesuma_6,
                                    KidneyAid = aid.Kidney,
                                    Scholarship = aid.Scholarship,
                                    EldersAid = aid.Elders,
                                    HealthAid = aid.Health,
                                    DisabilityAid = aid.Disability,
                                    OtherAids = aid.Others,
                                    Other = aid.OthersDetails,
                                    IsActive = true,
                                };

                                _context.AidDetail.Add(aidDetail);
                                
                            }
                            await this.SaveChangesAsync();
                        }

                        if (req.FamilyMember != null)
                        {
                            foreach (var member in req.FamilyMember)
                            {
                                var familyMember = new BNS.Entities.Entity.FamilyMember
                                {
                                    FamilyId = families.ID,
                                    FullName = member.FullName,
                                    NIC = member.Nic,
                                    BirthDay = Convert.ToDateTime(member.Birthday),
                                    Gender = member.Gender,
                                    MaritalStatus = member.MaritalStatus,
                                    Job = member.Job,
                                    PhoneNumber = member.Mobile,
                                    Income = Convert.ToDecimal(member.Income),
                                    IsActive = true,
                                };

                                _context.FamilyMember.Add(familyMember);
                                await this.SaveChangesAsync();

                                if (member.AidSelections != null)
                                {
                                    foreach (var aid in member.AidSelections)
                                    {
                                        var aidDetail = new BNS.Entities.Entity.AidDetail
                                        {
                                            MemberId = familyMember.ID,
                                            SpecialAids = aid.Special,
                                            Aswesuma_1 = aid.Aswesuma_1,
                                            Aswesuma_2 = aid.Aswesuma_2,
                                            Aswesuma_3 = aid.Aswesuma_3,
                                            Aswesuma_4 = aid.Aswesuma_4,
                                            Aswesuma_5 = aid.Aswesuma_5,
                                            Aswesuma_6 = aid.Aswesuma_6,
                                            KidneyAid = aid.Kidney,
                                            Scholarship = aid.Scholarship,
                                            EldersAid = aid.Elders,
                                            HealthAid = aid.Health,
                                            DisabilityAid = aid.Disability,
                                            OtherAids = aid.Others,
                                            Other = aid.OthersDetails,
                                            IsActive = true,
                                        };

                                        _context.AidDetail.Add(aidDetail);
                                        
                                    }
                                    await this.SaveChangesAsync();
                                }
                            }
                        }

                        if (req.FamilyChild != null)
                        {
                            foreach (var child in req.FamilyChild)
                            {
                                var familyChild = new BNS.Entities.Entity.FamilyMember
                                {
                                    FamilyId = families.ID,
                                    FullName = child.FullName,
                                    NIC = child.Nic,
                                    BirthDay = Convert.ToDateTime(child.Birthday),
                                    Gender = child.Gender,
                                    MaritalStatus = null,
                                    Job = null,
                                    PhoneNumber = null,
                                    Income = 0,
                                    IsActive = true,
                                };

                                _context.FamilyMember.Add(familyChild);
                                await this.SaveChangesAsync();

                                if (child.AidSelections != null)
                                {
                                    foreach (var aid in child.AidSelections)
                                    {
                                        var aidDetail = new BNS.Entities.Entity.AidDetail
                                        {
                                            MemberId = familyChild.ID,
                                            SpecialAids = aid.Special,
                                            Aswesuma_1 = aid.Aswesuma_1,
                                            Aswesuma_2 = aid.Aswesuma_2,
                                            Aswesuma_3 = aid.Aswesuma_3,
                                            Aswesuma_4 = aid.Aswesuma_4,
                                            Aswesuma_5 = aid.Aswesuma_5,
                                            Aswesuma_6 = aid.Aswesuma_6,
                                            KidneyAid = aid.Kidney,
                                            Scholarship = aid.Scholarship,
                                            EldersAid = aid.Elders,
                                            HealthAid = aid.Health,
                                            DisabilityAid = aid.Disability,
                                            OtherAids = aid.Others,
                                            Other = aid.OthersDetails,
                                            IsActive = true,
                                        };

                                        _context.AidDetail.Add(aidDetail);
                                        
                                    }
                                    await this.SaveChangesAsync();
                                }
                            }
                        }

                    }

                    return families.ID;
                }

                return 0;

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: SaveLeads " + e);
                return 0;
            }

        }

        public async Task<int> UpdateHouse(UpdateHouseDto req)
        {
            try
            {

                if (req.ID != 0)
                {
                    var family = await _context.Family.FirstOrDefaultAsync(lp => lp.ID == req.ID);

                    if (family != null)
                    {
                        family.FamilyNo = req.FamilyNo;
                        family.HouseUnitNo = req.HouseUnitNo;
                        family.SubNo = req.SubNo;
                        family.HouseholdNo = req.HouseholdNo;
                    }

                    await _context.SaveChangesAsync();

                    await this.AuditLog("Update House", $"Village No: {family.FamilyNo}, House Hold No: {family.SubNo}", _ICurrentUserService.Name, int.Parse(_ICurrentUserService.UserId));
                    return family.ID;
                }

                return 0;

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: Family Update" + e);
                return 0;
            }

        }

        public async Task<int> DeleteHouse(DeleteHouseDto req)
        {
            try
            {
                if (req.ID != 0)
                {
                    var family = await _context.Family.FirstOrDefaultAsync(lp => lp.IsActive && lp.ID == req.ID);

                    if (family != null)
                    {
                        family.IsActive = false;

                        var members = await _context.FamilyMember
                            .Where(x => x.IsActive && x.FamilyId == family.ID)
                            .ToListAsync();

                        foreach (var member in members)
                        {
                            member.IsActive = false;

                            var aids = await _context.AidDetail
                                .Where(x => x.IsActive && x.MemberId == member.ID)
                                .ToListAsync();

                            foreach (var aid in aids)
                            {
                                aid.IsActive = false;
                            }
                        }

                        await _context.SaveChangesAsync();

                        await this.AuditLog("Delete House", $"Village No: {family.FamilyNo}, House Hold No: {family.SubNo}", _ICurrentUserService.Name, int.Parse(_ICurrentUserService.UserId));
                        return family.ID;
                    }
                }

                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError("Error: Family Delete " + e.Message);
                return 0;
            }
        }

        public IEnumerable<ViewMemberDto> GetAllMembers(LeadsViewDto req)
        {
            try
            {
                if (req.UserRole == "Admin")
                {

                    var MembersList = _context.Family
                        .Where(c => c.IsActive)
                        .Select(l => new ViewMemberDto
                        {
                            ID = l.ID,
                            FamilyNo = l.FamilyNo,
                            HouseUnitNo = l.HouseUnitNo,
                            SubNo = l.SubNo,
                            HouseholdNo = l.HouseholdNo,
                            Wasama = l.GramaOffice.Name,
                            WasamNo = l.GramaOffice.LocationCode,
                            OfficerName = l.Officer.FullName
                        })
                        .ToList();

                    MembersList = [.. MembersList.OrderByDescending(x => x.ID)];

                    return MembersList;
                }
                else if (req.UserRole == "User")
                {

                    var MembersList = _context.Family
                        .Where(c => c.IsActive && c.OfficerId == req.UserId)
                        .Select(l => new ViewMemberDto
                        {
                            ID = l.ID,
                            FamilyNo = l.FamilyNo,
                            HouseUnitNo = l.HouseUnitNo,
                            SubNo = l.SubNo,
                            HouseholdNo = l.HouseholdNo,
                            Wasama = l.GramaOffice.Name,
                            WasamNo = l.GramaOffice.LocationCode,
                            OfficerName = l.Officer.FullName
                        })
                        .ToList();

                    MembersList = [.. MembersList.OrderByDescending(x => x.ID)];

                    return MembersList;
                }
                else
                {
                    return [];
                }

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: Get Family List " + e);
                throw;
            }
        }

        public async Task<ViewFamilyDataDto> GetHousebyID(int id)
        {
            try
            {
                if (id != 0)
                {
                    var house = await _context.Family
                        .Include(f => f.Members)
                        .Include(f => f.GramaOffice)
                        .Include(f => f.Officer)
                        .FirstOrDefaultAsync(f => f.IsActive && f.ID == id);

                    if (house == null)
                        return null;

                    var dto = new ViewFamilyDataDto
                    {
                        ID = house.ID,
                        FamilyNo = house.FamilyNo,
                        HouseUnitNo = house.HouseUnitNo,
                        SubNo = house.SubNo,
                        HouseholdNo = house.HouseholdNo,
                        Wasama = house.GramaOffice.Name,
                        WasamNo = house.GramaOffice.LocationCode,
                        OfficerName = house.Officer.FullName,
                        FamilyMembers = house.Members
                            .Where(m => m.IsActive)
                            .Select(m => new FMembersDto
                            {
                                id = m.ID,
                                FullName = m.FullName,
                                Nic = m.NIC,
                                Birthday = m.BirthDay,
                                MaritalStatus = m.MaritalStatus,
                                Gender = m.Gender,
                                Job = m.Job,
                                Mobile = m.PhoneNumber,
                                Income = m.Income,
                                IsChild = m.Job == null && m.MaritalStatus == null,
                                AidsList = _context.AidDetail
                                    .Where(a => a.MemberId == m.ID)
                                    .AsEnumerable()
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
                            }).ToList()
                    };

                    return dto;
                }

                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: GetHousebyID");
                return null;
            }
        }

    }
}

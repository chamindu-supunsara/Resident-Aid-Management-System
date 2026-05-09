using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SITS.BNS.Application.DTO.Family;
using SITS.BNS.Application.DTO.Members;
using SITS.BNS.Application.Repositories.Common;
using SITS.BNS.Application.Repositories.Interfaces;
using SITS.BNS.Entities.Common;
using SITS.BNS.Entities.Entity;
using SITS.BNS.Entities.Interfaces;
using SITS.BNS.Infrastructure;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.Repositories
{
    internal class FamilyMembersRepository(ApplicationDbContext context, ICurrentUserService iCurrentUserService, ILogger<UnitOfWork> logger) : Repository<FamilyMember>(context), IFamilyMembersRepository
    {
        private readonly ILogger<UnitOfWork> _logger = logger;
        private new readonly ApplicationDbContext _context = context;
        private readonly ICurrentUserService _ICurrentUserService = iCurrentUserService;

        public async Task<ViewMemberDataDto> GetMemberbyID(int id)
        {
            try
            {
                if (id != 0)
                {
                    var member = await _context.FamilyMember
                        .Include(m => m.AidDetail).FirstOrDefaultAsync(m => m.IsActive && m.ID == id);

                    if (member == null)
                        return null;

                    var dto = new ViewMemberDataDto
                    {
                        ID = member.ID,
                        FullName = member.FullName,
                        NIC = member.NIC,
                        BirthDay = member.BirthDay,
                        Gender = member.Gender,
                        MaritalStatus = member.MaritalStatus,
                        Job = member.Job,
                        PhoneNumber = member.PhoneNumber,
                        Income = member.Income,
                        FamilyID = member.FamilyId,

                        SpecialAids = member.AidDetail?.SpecialAids,
                        Other = member.AidDetail?.OtherAids,
                        Aswesuma_1 = member.AidDetail?.Aswesuma_1,
                        Aswesuma_2 = member.AidDetail?.Aswesuma_2,
                        Aswesuma_3 = member.AidDetail?.Aswesuma_3,
                        Aswesuma_4 = member.AidDetail?.Aswesuma_4,
                        Aswesuma_5 = member.AidDetail?.Aswesuma_5,
                        Aswesuma_6 = member.AidDetail?.Aswesuma_6,
                        Kidney = member.AidDetail?.KidneyAid,
                        Scholarship = member.AidDetail?.Scholarship,
                        Elders = member.AidDetail?.EldersAid,
                        Health = member.AidDetail?.HealthAid,
                        Disability = member.AidDetail?.DisabilityAid,

                        Others = member.AidDetail?.Other
                    };

                    return dto;
                }

                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: GetMemberbyID");
                return null;
            }
        }

        public async Task<int> UpdateMember(UpdateMemberDto req)
        {
            try
            {

                if (req.ID != 0)
                {
                    var member = await _context.FamilyMember.FirstOrDefaultAsync(lp => lp.IsActive && lp.ID == req.ID);

                    if (member == null)
                        return 0;

                    member.FullName = req.FullName;
                    member.NIC = string.IsNullOrWhiteSpace(req.Nic) ? null : req.Nic;
                    member.BirthDay = req.Birthday ?? DateTime.MinValue;
                    member.Gender = req.Gender;
                    member.MaritalStatus = string.IsNullOrWhiteSpace(req.MaritalStatus) ? null : req.MaritalStatus;
                    member.Job = string.IsNullOrWhiteSpace(req.Job) ? null : req.Job;
                    member.PhoneNumber = string.IsNullOrWhiteSpace(req.Mobile) ? null : req.Mobile;
                    member.Income = string.IsNullOrWhiteSpace(req.Income)
                        ? 0
                        : Convert.ToDecimal(req.Income);

                    var aids = await _context.AidDetail.FirstOrDefaultAsync(lp => lp.IsActive && lp.MemberId == member.ID);

                    if (aids == null)
                    {
                        aids = new AidDetail
                        {
                            MemberId = member.ID,
                            IsActive = true,
                        };
                        _context.AidDetail.Add(aids);
                    }

                    aids.SpecialAids = req.Special;
                    aids.KidneyAid = req.Kidney;
                    aids.HealthAid = req.Health;
                    aids.Scholarship = req.Scholarship;
                    aids.DisabilityAid = req.Disability;
                    aids.Aswesuma_1 = req.Aswesuma_1;
                    aids.Aswesuma_2 = req.Aswesuma_2;
                    aids.Aswesuma_3 = req.Aswesuma_3;
                    aids.Aswesuma_4 = req.Aswesuma_4;
                    aids.Aswesuma_5 = req.Aswesuma_5;
                    aids.Aswesuma_6 = req.Aswesuma_6;
                    aids.EldersAid = req.Elders;
                    aids.OtherAids = req.Others;

                    aids.Other = req.Others ? req.OthersDetails : null;

                    await _context.SaveChangesAsync();

                    await this.AuditLog("Edit Members", $"Update Family Member {member.FullName}", _ICurrentUserService.Name, int.Parse(_ICurrentUserService.UserId));
                    return member.ID;
                }

                return 0;

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: Family Update" + e);
                return 0;
            }

        }

        public async Task<int> AddMember(AddMemberDto req)
        {
            try
            {
                if (req.FamilyId == 0)
                    return 0;

                var family = await _context.Family.FirstOrDefaultAsync(f => f.IsActive && f.ID == req.FamilyId);
                if (family == null)
                    return 0;

                var birthDay = req.Birthday ?? DateTime.MinValue;

                var member = new FamilyMember
                {
                    FamilyId = req.FamilyId,
                    FullName = req.FullName,
                    NIC = req.Nic,
                    BirthDay = birthDay,
                    Gender = req.Gender,
                    MaritalStatus = req.IsChild ? null : req.MaritalStatus,
                    Job = req.IsChild ? null : req.Job,
                    PhoneNumber = req.IsChild ? null : req.Mobile,
                    Income = req.IsChild
                        ? 0
                        : (string.IsNullOrWhiteSpace(req.Income) ? 0 : Convert.ToDecimal(req.Income)),
                    IsActive = true,
                };

                _context.FamilyMember.Add(member);
                await _context.SaveChangesAsync();

                var aidDetail = new AidDetail
                {
                    MemberId = member.ID,
                    SpecialAids = req.Special,
                    Aswesuma_1 = req.Aswesuma_1,
                    Aswesuma_2 = req.Aswesuma_2,
                    Aswesuma_3 = req.Aswesuma_3,
                    Aswesuma_4 = req.Aswesuma_4,
                    Aswesuma_5 = req.Aswesuma_5,
                    Aswesuma_6 = req.Aswesuma_6,
                    KidneyAid = req.Kidney,
                    Scholarship = req.Scholarship,
                    EldersAid = req.Elders,
                    HealthAid = req.Health,
                    DisabilityAid = req.Disability,
                    OtherAids = req.Others,
                    Other = req.Others ? req.OthersDetails : null,
                    IsActive = true,
                };

                _context.AidDetail.Add(aidDetail);
                await _context.SaveChangesAsync();

                var activeCount = await _context.FamilyMember.CountAsync(m => m.IsActive && m.FamilyId == req.FamilyId);
                family.HouseholdNo = activeCount.ToString(CultureInfo.InvariantCulture);
                await _context.SaveChangesAsync();

                await this.AuditLog("Add Member", $"Add Family Member {member.FullName}", _ICurrentUserService.Name, int.Parse(_ICurrentUserService.UserId));
                return member.ID;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: AddMember");
                return 0;
            }
        }

        public async Task<int> DeleteMember(DeleteMemberDto req)
        {
            try
            {

                if (req.ID != 0)
                {
                    var member = await _context.FamilyMember.FirstOrDefaultAsync(lp => lp.IsActive && lp.ID == req.ID);

                    if (member != null)
                    {
                        member.IsActive = false;

                    }

                    var aids = await _context.AidDetail.FirstOrDefaultAsync(lp => lp.IsActive && lp.MemberId == member!.ID);

                    if (aids != null)
                    {
                        aids.IsActive = false;

                    }

                    await _context.SaveChangesAsync();

                    var family = await _context.Family.FirstOrDefaultAsync(f => f.IsActive && f.ID == member.FamilyId);
                    if (family != null)
                    {
                        var activeCount = await _context.FamilyMember.CountAsync(m => m.IsActive && m.FamilyId == family.ID);
                        family.HouseholdNo = activeCount.ToString(CultureInfo.InvariantCulture);
                        await _context.SaveChangesAsync();
                    }

                    await this.AuditLog("Delete Member", $"Family Member {member.FullName}", _ICurrentUserService.Name, int.Parse(_ICurrentUserService.UserId));
                    return member.ID;
                }

                return 0;

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: Family Update" + e);
                return 0;
            }

        }

    }
}

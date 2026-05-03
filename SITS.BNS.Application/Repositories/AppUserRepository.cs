using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Org.BouncyCastle.Ocsp;
using SITS.BNS.Application.DTO.AppUsers;
using SITS.BNS.Application.DTO.AuditLog;
using SITS.BNS.Application.DTO.Company;
using SITS.BNS.Application.DTO.Family;
using SITS.BNS.Application.DTO.GramaOffice;
using SITS.BNS.Application.DTO.Leads;
using SITS.BNS.Application.Repositories.Common;
using SITS.BNS.Application.Repositories.Interfaces;
using SITS.BNS.Common;
using SITS.BNS.Common.Exceptions;
using SITS.BNS.Entities.AuthModels;
using SITS.BNS.Entities.Common;
using SITS.BNS.Entities.Entity;
using SITS.BNS.Entities.Interfaces;
using SITS.BNS.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.Repositories
{
    public class AppUserRepository(ApplicationDbContext context, IOptions<AppSettings> appSettings, ILogger<UnitOfWork> logger, ICurrentUserService currentUserService) : Repository<AppUser>(context), IAppUserRepository
    {
        private new readonly ApplicationDbContext _context = context;
        private readonly IOptions<AppSettings> _appSettings = appSettings;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ILogger<UnitOfWork> _logger = logger;

        public async Task<int> SaveAppUser(AppUser appUser)
        {
            this.Add(appUser);
            return await this.SaveChangesAsync();
        }

        public async Task<int> RegisterUsers(AppUserRegisterDto req)
        {
            try
            {
                var users = new BNS.Entities.Entity.AppUser
                {
                    UserEmail = req.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(req.Password),
                    FullName = req.Firstname + " " + req.Lastname,
                    FirstName = req.Firstname,
                    LastName = req.Lastname,
                    Mobile = req.Mobile,
                    Organization = req.CompanyName,
                    OrganizationId = req.GramaID,
                    GramaOfficeId = req.CompanyID,
                    IsActive = false,
                };

                _context.AppUsers.Add(users);
                await _context.SaveChangesAsync();

                if (users.ID > 0)
                {
                    var userrole = new BNS.Entities.Entity.AppUserRole
                    {
                        AppUserId = users.ID,
                        AppRoleId = 2,
                        IsActive = true,
                    };

                    _context.AppUserRoles.Add(userrole);

                    var grama = await _context.GramaOffice.FirstOrDefaultAsync(lp => lp.ID == req.CompanyID);

                    if (grama != null)
                    {
                        grama.IsAvailable = false;
                    }

                    await _context.SaveChangesAsync();

                }

                await this.AuditLog("Sign Up", $"Register User {users.FullName}", _currentUserService.Name, int.Parse(_currentUserService.UserId));
                return users.ID;

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: Users Register" + e);
                return 0;
            }

        }

        public async Task<int> UpdateAppUsers(AppUserUpdateDto req)
        {
            try
            {
                
                if (req.ID != 0)
                {
                    var officer = await _context.AppUsers.FirstOrDefaultAsync(lp => lp.ID == req.ID);

                    if (officer != null)
                    {
                        officer.FullName = $"{req.Firstname} {req.Lastname}".Trim();
                        officer.FirstName = req.Firstname;
                        officer.LastName = req.Lastname;
                        officer.UserEmail = req.Email;
                        officer.Mobile = req.Mobile;
                        officer.IsActive = req.IsActive ?? officer.IsActive;
                    }

                    await _context.SaveChangesAsync();

                    await this.AuditLog("Update User", $"Edit User {officer.FullName}", _currentUserService.Name, int.Parse(_currentUserService.UserId));
                    return officer.ID;
                }

                return 0;

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: Officers Update" + e);
                return 0;
            }

        }

        public async Task<int> DeleteAppUser(int id)
        {
            try
            {
                if (id != 0)
                {
                    var officer = await _context.AppUsers.FirstOrDefaultAsync(lp => lp.ID == id);

                    if (officer != null)
                    {

                        var grama = await _context.GramaOffice.FirstOrDefaultAsync(lp => lp.ID == officer.GramaOfficeId);
                        _context.AppUsers.Remove(officer);

                        if (grama != null)
                        {
                            grama.IsAvailable = true;
                        }

                        await _context.SaveChangesAsync();

                        await this.AuditLog("Admin Delete", $"Delete User {officer.FullName}", _currentUserService.Name, int.Parse(_currentUserService.UserId));
                        return officer.ID;

                    }

                    return 0;
                }

                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deleting AppUser with ID: {UserId}", id);
                return 0;
            }
        }

        public async Task<int> UpdatePassword(AppUserUpdatePassword req)
        {
            try
            {
                if (req.UserEmail != null)
                {
                    var user = await _context.AppUsers.FirstOrDefaultAsync(lp => lp.UserEmail == req.UserEmail);

                    if (user == null)
                    {
                        return 0;
                    }

                    user.Password = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);
                    await this.SaveChangesAsync();

                    await this.AuditLog("Password", $"Change Password {user.FullName}", _currentUserService.Name, int.Parse(_currentUserService.UserId));
                    return user.ID;
                }

                return 0;

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: Update Password " + e);
                return 0;
            }

        }

        public async Task<AuthResponse> Authenticate(AuthRequest authRequest)
        {
            var user = this.Find(x => x.IsActive && x.UserEmail == authRequest.USER_ID).FirstOrDefault();

            if (user != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(authRequest.USER_PASSWORD, user.Password);
                if (verified)
                {
                    var authres = new AuthResponse();
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes(this._appSettings.Value.AuthConfig.Key);
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Name,user.FullName),
                        new Claim("UserId", user.ID.ToString()),
                        new Claim("AccessType", "InApp"),
                        new Claim("Email", authRequest.USER_ID),
                        new Claim (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(this._appSettings.Value.AuthConfig.ExpiresIn),
                        SigningCredentials = new
                        SigningCredentials(
                            new SymmetricSecurityKey(tokenKey),
                            SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    authres.Success = true;
                    authres.RefreshToken = String.Empty;
                    authres.JWToken = tokenHandler.WriteToken(token);
                    authres.UserId = user.ID.ToString();
                    authres.Email = authRequest.USER_ID.ToString();
                    authres.Name = user.FullName;
                    authres.OrganizationId = user.OrganizationId.ToString();

                    return await Task.FromResult(authres);

                }
                else
                {
                    throw new NotFoundException("Invalid Credentials");
                }
            }
            else
            {
                throw new NotFoundException("User not Active");
            }

        }

        public IEnumerable<ViewOfficersDto> GetAllOfficers(LeadsViewDto req)
        {
            try
            {
                if (req.UserRole == "Admin")
                {

                    var MembersList = _context.AppUsers
                        .Select(l => new ViewOfficersDto
                        {
                            ID = l.ID,
                            Firstname = l.FirstName,
                            UserEmail = l.UserEmail,
                            WasamName = l.Organization,
                            Lastname = l.LastName,
                            Mobile = l.Mobile,
                            Status = l.IsActive
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
                _logger.LogInformation("Error: Get Officers List " + e);
                throw;
            }
        }

        public IEnumerable<AllAuditDto> GetAllAuditLogs()
        {
            try
            {
                var AuditLogs = _context.AuditLog
                        .Select(l => new AllAuditDto
                        {
                            ID = l.ID,
                            Action = l.Action,
                            Category = l.Category,
                            EditedBy = l.EditBy,
                            CreatedDate = l.CreatedDate.HasValue ? l.CreatedDate.Value.AddHours(12).AddMinutes(30) : (DateTime?)null,
                            UpdatedDate = l.UpdatedDate.HasValue ? l.UpdatedDate.Value.AddHours(12.5).AddMinutes(30) : (DateTime?)null,
                            UserId = l.AppUserID
                        })
                        .ToList();

                AuditLogs = [.. AuditLogs.OrderByDescending(x => x.ID)];

                return AuditLogs;

            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: Get All Audit Logs" + e);
                throw;
            }

        }

        public async Task<bool> CheckEmailExists(AppUserCheckEmailDto appUserCheckEmailDto)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.UserEmail == appUserCheckEmailDto.UserEmail);

            return user != null;
        }

    }
}

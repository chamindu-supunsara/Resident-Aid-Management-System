using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SITS.BNS.Application;
using SITS.BNS.Application.DTO.AppUsers;
using SITS.BNS.Common.Interfaces;
using SITS.BNS.Entities.AuthModels;
using SITS.BNS.Entities.Common;
using SITS.BNS.Entities.Entity;
using SITS.BNS.Entities.Interfaces;

namespace SITS.BNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IUnitOfWork unitOfWork, IApplicationLogger applicationLogger, ICurrentUserService currentUserService, IOptions<AuthConfig> authConfig) : ControllerBase
    {
        private readonly IApplicationLogger _logger = applicationLogger;
        private readonly string requestapath = "/LoginController/";
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IOptions<AuthConfig> _authConfig = authConfig;

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthRequest authRequest)
        {
            var authres = new AuthResponse();
            AuthResponse authresp = null;

            try
            {
                if (authRequest.TYPE == "InApp")
                {
                    if (string.IsNullOrEmpty(authRequest.USER_ID) || string.IsNullOrEmpty(authRequest.USER_PASSWORD))
                    {
                        authres.Success = false;
                        authres.Message = "Invalid credentials, Please recheck your credentials!";
                        return Ok(authres);
                    }

                    authresp = await _unitOfWork.AppUsers.Authenticate(authRequest);
                }
                else
                {
                    authres.Success = false;
                    authres.Message = "Unsupported login type.";
                    return Ok(authres);
                }

                if (authresp == null || string.IsNullOrEmpty(authresp.UserId))
                {
                    authres.Success = false;
                    authres.Message = "Authentication failed.";
                    return Ok(authres);
                }

                var refreshToken = GenerateRefreshToken(int.Parse(authresp.UserId));

                if (refreshToken == null)
                {
                    authres.Success = false;
                    authres.Message = "Error generating refresh token";
                    return Ok(authres);
                }

                await _unitOfWork.RefreshTokens.AddRefreshToken(refreshToken);

                var userRoles = await _unitOfWork.UserRoles.GetAllUserRoleByUserId(int.Parse(authresp.UserId));
                var roleNames = new List<string>();

                if (userRoles != null && userRoles.Count >= 1)
                {
                    foreach (var userRole in userRoles)
                    {
                        var role = await _unitOfWork.Roles.GetRoleById(userRole.AppRoleId);
                        if (role != null)
                        {
                            roleNames.Add(role.Name);
                        }
                    }
                }

                authres.Success = true;
                authres.RefreshToken = refreshToken.Token;
                authres.JWToken = authresp.JWToken;
                authres.UserId = authresp.UserId;
                authres.Email = authresp.Email;
                authres.Name = authresp.Name;
                authres.AuthType = authRequest.TYPE;
                authres.OrganizationId = authresp.OrganizationId;
                authres.Message = "Success";
                authres.Roles = roleNames;

                return Ok(authres);
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Authenticate User",
                    Description = $"{ex.Message}{(ex.InnerException != null ? $" Inner exception: {ex.InnerException.Message}" : "")}",
                    UserID = _currentUserService.UserId,
                    TimeStamp = DateTime.Now,
                    AccessType = "",
                    Message = ex.Message,
                    RequestType = requestapath,
                    Severity = "High",
                    StatusCode = 500
                });

                authres.Success = false;
                authres.Message = "Error occurred during login!";
                return Ok(authres);
            }
        }

        [AllowAnonymous]
        [HttpPost("RegisterUsers")]
        public async Task<int> RegisterUsers(AppUserRegisterDto req)
        {
            try
            {
                if (req.ID == 0)
                {
                    var User = await _unitOfWork.AppUsers.RegisterUsers(req);
                    return User;
                }

                return 0;
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Save Users",
                    Description = $"{ex.Message}{(ex.InnerException != null ? $" Inner exception: {ex.InnerException.Message}" : "")}",
                    UserID = _currentUserService.UserId,
                    TimeStamp = DateTime.Now,
                    AccessType = "",
                    Message = ex.Message,
                    RequestType = requestapath,
                    Severity = "High",
                    StatusCode = 500
                });

                throw;
            }
        }

        [AllowAnonymous]
        [HttpGet("CheckEmailExists")]
        public async Task<IActionResult> CheckEmailExists([FromQuery] AppUserCheckEmailDto req)
        {
            try
            {
                var emailExists = await _unitOfWork.AppUsers.CheckEmailExists(req);
                return Ok(new { emailExists });

            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Check Email Exists",
                    Description = $"{ex.Message}{(ex.InnerException != null ? $" Inner exception: {ex.InnerException.Message}" : "")}",
                    UserID = _currentUserService.UserId,
                    TimeStamp = DateTime.Now,
                    AccessType = "",
                    Message = ex.Message,
                    RequestType = requestapath,
                    Severity = "High",
                    StatusCode = 500
                });

                return new ObjectResult(new { error = ex.Message })
                {
                    StatusCode = 500
                };
            }
        }

        [AllowAnonymous]
        [HttpPost("UpdatePassword")]
        public async Task<int> UpdatePassword(AppUserUpdatePassword req)
        {
            try
            {
                if (req.UserEmail != null)
                {
                    var pwd = await _unitOfWork.AppUsers.UpdatePassword(req);
                    return pwd;
                }

                return 0;
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Update Password",
                    Description = $"{ex.Message}{(ex.InnerException != null ? $" Inner exception: {ex.InnerException.Message}" : "")}",
                    UserID = _currentUserService.UserId,
                    TimeStamp = DateTime.Now,
                    AccessType = "",
                    Message = ex.Message,
                    RequestType = requestapath,
                    Severity = "High",
                    StatusCode = 500
                });

                throw;
            }
        }

        private RefreshToken? GenerateRefreshToken(int userId)
        {
            if (_authConfig == null) return null;

            if (_authConfig.Value.RefreshTokenExpirationDays == 0) return null;

            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(_authConfig.Value.RefreshTokenExpirationDays)
            };

            return refreshToken;
        }
    }
}

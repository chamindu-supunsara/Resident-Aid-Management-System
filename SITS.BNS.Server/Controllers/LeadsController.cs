using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SITS.BNS.Application;
using SITS.BNS.Application.DTO.AppUsers;
using SITS.BNS.Application.DTO.Family;
using SITS.BNS.Application.DTO.Leads;
using SITS.BNS.Application.DTO.Members;
using SITS.BNS.Common.Interfaces;
using SITS.BNS.Entities.Interfaces;

namespace SITS.BNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController(IUnitOfWork unitOfWork, IApplicationLogger applicationLogger, ICurrentUserService currentUserService) : ControllerBase
    {
        private readonly IApplicationLogger _logger = applicationLogger;
        private readonly string requestapath = "/LeadsController/";
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [AllowAnonymous]
        [HttpPost("SaveLeads")]
        public async Task<int> SaveLeadForm(LeadSaveDto req)
        {
            try
            {
                if (req.ID == 0)
                {
                    var lead = await _unitOfWork.Family.SaveLeadForm(req);
                    return lead;
                }

                return 0;
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Save Leads",
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
        [HttpGet("GetDashboardStatus")]
        public async Task<IActionResult> GetDashboardStatus([FromQuery] LeadsViewDto req)
        {
            try
            {
                var result = _unitOfWork.Leads.GetDashboardStatus(req);
                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "View Dashboard Status",
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
        [HttpGet("GetDashboardPie")]
        public async Task<IActionResult> GetDashboardPie([FromQuery] LeadsViewDto req)
        {
            try
            {
                var result = _unitOfWork.Leads.GetDashboardPie(req);
                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "View Dashboard Pie Chart",
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
        [HttpGet("GetDashboardBar")]
        public async Task<IActionResult> GetDashboardBar([FromQuery] LeadsViewDto req)
        {
            try
            {
                var result = _unitOfWork.Leads.GetDashboardBar(req);
                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "View Dashboard Bar Chart",
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
        [HttpGet("GetAllMembers")]
        public async Task<IActionResult> GetAllMembers([FromQuery] LeadsViewDto req)
        {
            try
            {
                var result = _unitOfWork.Family.GetAllMembers(req);
                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Gt All Families",
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
        [HttpGet("GetAllAids")]
        public async Task<IActionResult> GetAllAids([FromQuery] LeadsViewDto req)
        {
            try
            {
                var result = _unitOfWork.AidDetails.GetAllAids(req);
                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Gt All Members",
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
        [HttpGet("GetAllOfficers")]
        public async Task<IActionResult> GetAllOfficers([FromQuery] LeadsViewDto req)
        {
            try
            {
                var result = _unitOfWork.AppUsers.GetAllOfficers(req);
                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Gt All Officers",
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
        [HttpGet("GetMemberbyID")]
        public async Task<IActionResult> GetMemberbyID([FromQuery] int id)
        {
            try
            {
                var result = await _unitOfWork.FamilyMembers.GetMemberbyID(id);

                if (result == null)
                {
                    return NotFound(new { message = "Member not found" });
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Get Member Details",
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
        [HttpGet("GetHousebyID")]
        public async Task<IActionResult> GetHousebyID([FromQuery] int id)
        {
            try
            {
                var result = await _unitOfWork.Family.GetHousebyID(id);

                if (result == null)
                {
                    return NotFound(new { message = "House not found" });
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Get House Details",
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
        [HttpGet("GetAllAuditLogs")]
        public async Task<IActionResult> GetAllAuditLogs()
        {
            try
            {
                var result = _unitOfWork.AppUsers.GetAllAuditLogs();
                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "View All Audit Logs",
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
        [HttpPost("UpdateAppUsers")]
        public async Task<int> UpdateAppUsers(AppUserUpdateDto req)
        {
            try
            {
                if (req.ID != 0)
                {
                    var lead = await _unitOfWork.AppUsers.UpdateAppUsers(req);
                    return lead;
                }

                return 0;
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Update Leads Status",
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
        [HttpPost("UpdateHouse")]
        public async Task<int> UpdateHouse(UpdateHouseDto req)
        {
            try
            {
                if (req.ID != 0)
                {
                    var lead = await _unitOfWork.Family.UpdateHouse(req);
                    return lead;
                }

                return 0;
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Update House",
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
        [HttpPost("UpdateMember")]
        public async Task<int> UpdateMember(UpdateMemberDto req)
        {
            try
            {
                if (req.ID != 0)
                {
                    var lead = await _unitOfWork.FamilyMembers.UpdateMember(req);
                    return lead;
                }

                return 0;
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Update Member",
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
        [HttpPost("DeleteHouse")]
        public async Task<int> DeleteHouse(DeleteHouseDto req)
        {
            try
            {
                if (req.ID != 0)
                {
                    var lead = await _unitOfWork.Family.DeleteHouse(req);
                    return lead;
                }

                return 0;
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Update House",
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
        [HttpPost("DeleteMember")]
        public async Task<int> DeleteMember(DeleteMemberDto req)
        {
            try
            {
                if (req.ID != 0)
                {
                    var lead = await _unitOfWork.FamilyMembers.DeleteMember(req);
                    return lead;
                }

                return 0;
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Update Member",
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
        [HttpDelete("DeleteAppUser/{id}")]
        public async Task<int> DeleteAppUser(int id)
        {
            try
            {
                if (id != 0)
                {
                    var lead = await _unitOfWork.AppUsers.DeleteAppUser(id);
                    return lead;
                }

                return 0;
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "Update Leads Status",
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

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SITS.BNS.Application;
using SITS.BNS.Common.Interfaces;
using SITS.BNS.Entities.Interfaces;

namespace SITS.BNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(IUnitOfWork unitOfWork, IApplicationLogger applicationLogger, ICurrentUserService currentUserService) : ControllerBase
    {
        private readonly IApplicationLogger _logger = applicationLogger;
        private readonly string requestapath = "/CompanyController/";
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [AllowAnonymous]
        [HttpGet("GetAllCompanyIsAvailable")]
        public async Task<IActionResult> GetAllCompanyIsAvailable()
        {
            try
            {
                var result = _unitOfWork.GramaOffice.GetAllCompanyIsAvailable();
                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, new Entities.Common.LogFormat
                {
                    Request = "View All Company",
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
    }
}

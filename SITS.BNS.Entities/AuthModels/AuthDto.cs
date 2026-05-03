using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.AuthModels
{
    public class AuthRequest
    {
        public string TYPE { get; set; }
        public string USER_ID { get; set; }
        public string USER_PASSWORD { get; set; }
    }

    public class AuthOTPRequest
    {
        public string OTP { get; set; }
        public string USER_ID { get; set; }
    }

    public class AuthReq
    {
        public string? UserEmail { get; set; }
        public string? Password { get; set; }
        public string? Type { get; set; }
    }

    public class AuthRequestAPIRequest
    {
        public string RequestTime { get; set; } = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffZ");

        public string? UserName { get; set; }

        public string? UserPassword { get; set; }

    }

    public class RefreshRequest
    {
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
    }

    public class AuthResponse
    {
        public bool Success { get; set; }
        public string JWToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
        public string AccessType { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public List<string>? Roles { get; set; }
        public string AuthType { get; set; }
        public string OrganizationId { get; set; }
    }

    public class AdminAuthRequest
    {
        public string? USER_ID { get; set; }
        public string? USER_PASSWORD { get; set; }
    }

    public class AdminAuthResponse
    {
        public bool Success { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? UserID { get; set; }
        public string? PermissionLevel { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}

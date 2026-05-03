using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.Common
{
    public class AppSettings
    {
        public AuthConfig AuthConfig { get; set; }
        public TimerJobConfig TimerJobConfig { get; set; }
        public OtherConfig OtherConfig { get; set; }
        public SMTPConfig? SMTPConfig { get; set; }
        public O365Config O365Config { get; set; }
    }

    public class O365Config
    {
        public string? o365ClientId { get; set; }
        public string? o365TenantId { get; set; }
        public string? o365RedirectUrl { get; set; }
        public string? pkey { get; set; }
        public bool O365EMailNotifications { get; set; }
        public bool IsO365Support { get; set; }
    }

    public class AuthConfig
    {
        public string Key { get; set; }
        public int ExpiresIn { get; set; }
        public int WebExpiresIn { get; set; }
        public int MobileExpiresIn { get; set; }
        public string? TokenID { get; set; }
        public string? RefreshTokenKey { get; set; }
        public int RefreshTokenExpirationDays { get; set; }
        public string? JwtIssuer { get; set; }
    }

    public class OtherConfig
    {
        public string SubSite { get; set; }

    }

    public class TimerJobConfig
    {
        public string HostUrl { get; set; }
        public int OverdueDaysValidation { get; set; }
    }

    public class SMTPConfig
    {
        public bool EmailNotifications { get; set; }
        public string? Host { get; set; }
        public string? From { get; set; }
        public string? Alias { get; set; }
        public string? Password { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
    }

}

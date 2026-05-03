using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Entities.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AccessType { get; set; }
    }

    public interface ICommonService
    {
        Task<string> GetBase64ImgUrl(string? ImagePath);
        DateTime GetDate(string strdate);
        Task<bool> IsBase64(string? base64String);
        Task<string> GetImgPath(string? ImagePath, int UserId, string? type, string? subtype);
        DateTime? dateToday();
        DateTime? dateTimeToday();
    }
}

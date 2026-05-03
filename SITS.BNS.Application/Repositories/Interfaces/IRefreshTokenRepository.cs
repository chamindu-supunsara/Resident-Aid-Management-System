using SITS.BNS.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Application.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshToken(RefreshToken refreshToken);
        Task UpdateRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken?> GetRefreshToken(int userId, string token);
    }
}

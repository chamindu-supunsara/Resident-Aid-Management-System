using Microsoft.EntityFrameworkCore;
using SITS.BNS.Application.Repositories.Common;
using SITS.BNS.Application.Repositories.Interfaces;
using SITS.BNS.Entities.Entity;
using SITS.BNS.Infrastructure;

namespace SITS.BNS.Application.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public async Task AddRefreshToken(RefreshToken refreshToken)
        {
            refreshToken.IsActive = true;
            await AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRefreshToken(RefreshToken refreshToken)
        {
            try
            {
                Update(refreshToken);
                await SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RefreshToken?> GetRefreshToken(int userId, string token)
        {
            return await GetSingleOrDefaultAsync(r => r.UserId == userId && r.Token == token);
        }
    }
}

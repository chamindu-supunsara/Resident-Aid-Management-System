using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SITS.BNS.Application.Repositories;
using SITS.BNS.Application.Repositories.Interfaces;
using SITS.BNS.Entities.Common;
using SITS.BNS.Entities.Interfaces;
using SITS.BNS.Infrastructure;

namespace SITS.BNS.Application
{
    public class UnitOfWork(ApplicationDbContext context, IHttpContextAccessor httpAccessor, IOptions<AppSettings> appSettings, ICurrentUserService iCurrentUserService, ILogger<UnitOfWork> logger) : IUnitOfWork
    {
        readonly ApplicationDbContext _context = context;
        private readonly ICurrentUserService _ICurrentUserService = iCurrentUserService;
        private readonly IOptions<AppSettings> _appSettings = appSettings;
        private readonly ILogger<UnitOfWork> _logger = logger;
        private readonly IHttpContextAccessor _httpcontext = httpAccessor;
        IAppUserRepository? _appUserRepository;
        IRefreshTokenRepository? _refreshTokenRepository;
        IAppUserRoleRepository? _appUserRoleRepository;
        IAppRoleRepository? _appRoleRepository;
        ILeadsRepository? _leadsRepository;
        IGramaOfficeRepository? _gramaOfficeRepository;
        IAidDetailsRepository? _aidDetailsRepository;
        IFamilyRepository? _familyRepository;
        IFamilyMembersRepository? _familyMembersRepository;

        public IAppUserRepository AppUsers
        {
            get
            {
                if (_appUserRepository == null)
                    _appUserRepository = new AppUserRepository(_context, _appSettings, _logger, _ICurrentUserService);
                return _appUserRepository;
            }
        }

        public IRefreshTokenRepository RefreshTokens
        {
            get
            {
                if (_refreshTokenRepository == null)
                    _refreshTokenRepository = new RefreshTokenRepository(_context);
                return _refreshTokenRepository;
            }
        }

        public IAppUserRoleRepository UserRoles
        {
            get
            {
                if (_appUserRoleRepository == null)
                    _appUserRoleRepository = new AppUserRoleRepository(_context);
                return _appUserRoleRepository;
            }
        }

        public IAppRoleRepository Roles
        {
            get
            {
                if (_appRoleRepository == null)
                    _appRoleRepository = new AppRoleRepository(_context);
                return _appRoleRepository;
            }
        }

        public ILeadsRepository Leads
        {
            get
            {
                if (_leadsRepository == null)
                    _leadsRepository = new LeadsRepository(_context, _logger);
                return _leadsRepository;
            }
        }

        public IGramaOfficeRepository GramaOffice
        {
            get
            {
                if (_gramaOfficeRepository == null)
                    _gramaOfficeRepository = new GramaOfficeRepository(_context, _logger);
                return _gramaOfficeRepository;
            }
        }

        public IAidDetailsRepository AidDetails
        {
            get
            {
                if (_aidDetailsRepository == null)
                    _aidDetailsRepository = new AidDetailsRepository(_context, _logger);
                return _aidDetailsRepository;
            }
        }

        public IFamilyRepository Family
        {
            get
            {
                if (_familyRepository == null)
                    _familyRepository = new FamilyRepository(_context, _ICurrentUserService, _logger);
                return _familyRepository;
            }
        }

        public IFamilyMembersRepository FamilyMembers
        {
            get
            {
                if (_familyMembersRepository == null)
                    _familyMembersRepository = new FamilyMembersRepository(_context, _ICurrentUserService, _logger);
                return _familyMembersRepository;
            }
        }
        
        public ApplicationDbContext Context => _context;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}

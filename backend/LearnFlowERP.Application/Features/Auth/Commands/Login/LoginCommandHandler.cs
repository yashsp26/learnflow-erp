using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Auth.Commands.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LearnFlowERP.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler
    : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokenService;
        private readonly IMemoryCache _cache;

        public LoginCommandHandler(
            IApplicationDbContext context,
            IPasswordHasher hasher,
            ITokenService tokenService,
            IMemoryCache cache)
        {
            _context = context;
            _hasher = hasher;
            _tokenService = tokenService;
            _cache = cache;
        }

        public async Task<AuthResponseDto> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var email = request.Email.ToLower();

            var tenant = await _cache.GetOrCreateAsync(
                $"tenant_{request.TenantCode}",
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

                    return await _context.Tenants
                        .AsNoTracking()
                        .FirstOrDefaultAsync(t => t.Code == request.TenantCode);
                });

            if (tenant == null)
                throw new Exception("Invalid tenant");
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u =>
                    u.Email.ToLower() == email &&
                    u.TenantId == tenant.TenantId);


            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var isValid = _hasher.Verify(request.Password, user.PasswordHash);

            if (!isValid)
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = await _tokenService.GenerateTokenAsync(user);
            
            var refreshTokenValue = _tokenService.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                UserId = user.UserId,
                TenantId = user.TenantId,
                Token = refreshTokenValue,
                ExpiresAt = DateTime.Now.AddDays(7),
                IsRevoked = false
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshTokenValue,
                UserId = user.UserId,
                Email = user.Email
            };
        }
    }
}

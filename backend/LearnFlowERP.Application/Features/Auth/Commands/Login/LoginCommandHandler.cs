using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Auth.Commands.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

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
            var stopwatch = Stopwatch.StartNew();

            var email = request.Email.Trim();

            Console.WriteLine(
                $"[LOGIN] Started - {stopwatch.ElapsedMilliseconds} ms");

            var tenant = await _cache.GetOrCreateAsync(
                $"tenant_{request.TenantCode}",
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromMinutes(30);

                    return await _context.Tenants
                        .AsNoTracking()
                        .FirstOrDefaultAsync(
                            t => t.Code == request.TenantCode,
                            cancellationToken);
                });

            Console.WriteLine(
                $"[LOGIN] Tenant Lookup - {stopwatch.ElapsedMilliseconds} ms");

            if (tenant == null)
                throw new InvalidOperationException(
                    "Invalid tenant");

            var user = await _context.Users
                .AsNoTracking()
                .Select(x => new
                {
                    User = x,
                    RoleId = x.UserRoles
                        .Select(r => r.RoleId)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync(x =>
                    x.User.Email == email &&
                    x.User.TenantId == tenant.TenantId,
                    cancellationToken);

            Console.WriteLine(
                $"[LOGIN] User Lookup - {stopwatch.ElapsedMilliseconds} ms");

            if (user == null)
                throw new UnauthorizedAccessException(
                    "Invalid credentials");

            var isValid =
                _hasher.Verify(
                    request.Password,
                    user.User.PasswordHash);

            Console.WriteLine(
                $"[LOGIN] Password Verify - {stopwatch.ElapsedMilliseconds} ms");

            if (!isValid)
                throw new UnauthorizedAccessException(
                    "Invalid credentials");

            var accessToken =
                _tokenService.GenerateToken(
                    user.User,
                    user.RoleId);

            Console.WriteLine(
                $"[LOGIN] Token Generation - {stopwatch.ElapsedMilliseconds} ms");

            var refreshTokenValue =
                _tokenService.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                UserId = user.User.UserId,
                TenantId = user.User.TenantId,
                Token = refreshTokenValue,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            _context.RefreshTokens.Add(refreshToken);

            await _context.SaveChangesAsync(
                cancellationToken);

            Console.WriteLine(
                $"[LOGIN] SaveChanges - {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine(
                $"[LOGIN] TOTAL - {stopwatch.ElapsedMilliseconds} ms");

            return new AuthResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshTokenValue,
                UserId = user.User.UserId,
                Email = user.User.Email
            };
        }
    }
}
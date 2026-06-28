using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Auth.Commands.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Auth.Commands.RefreshTokenCommands
{
    public class RefreshTokenCommandHandler
        : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(
            IApplicationDbContext context,
            ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var refreshToken = await _context.RefreshTokens
                .AsNoTracking()
                .Where(x =>
                    x.Token == request.RefreshToken &&
                    !x.IsRevoked &&
                    x.ExpiresAt > DateTime.UtcNow)
                .Select(x => new
                {
                    RefreshToken = x,
                    User = x.User,
                    RoleId = x.User.UserRoles
                        .Select(r => r.RoleId)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (refreshToken == null)
                throw new UnauthorizedAccessException(
                    "Invalid refresh token");

            // Revoke old token
            var existingToken = await _context.RefreshTokens
                .FirstAsync(
                    x => x.RefreshTokenId ==
                         refreshToken.RefreshToken.RefreshTokenId,
                    cancellationToken);

            existingToken.IsRevoked = true;

            var accessToken =
                _tokenService.GenerateToken(
                    refreshToken.User,
                    refreshToken.RoleId);

            var newRefreshToken =
                _tokenService.GenerateRefreshToken();

            _context.RefreshTokens.Add(
                new RefreshToken
                {
                    UserId = refreshToken.User.UserId,
                    TenantId = refreshToken.User.TenantId,
                    Token = newRefreshToken,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    IsRevoked = false
                });

            await _context.SaveChangesAsync(
                cancellationToken);

            return new AuthResponseDto
            {
                Token = accessToken,
                RefreshToken = newRefreshToken,
                UserId = refreshToken.User.UserId,
                Email = refreshToken.User.Email
            };
        }
    }
}
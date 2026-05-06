using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
            var token = await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r =>
                    r.Token == request.RefreshToken &&
                    !r.IsRevoked &&
                    r.ExpiresAt > DateTime.Now);

            if (token == null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            // rotate token (best practice)
            token.IsRevoked = true;

            var newAccessToken = await _tokenService.GenerateTokenAsync(token.User);

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = token.UserId,
                TenantId = token.TenantId,
                Token = newRefreshToken,
                ExpiresAt = DateTime.Now.AddDays(7)
            };

            _context.RefreshTokens.Add(refreshTokenEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return new AuthResponseDto
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                UserId = token.User.UserId,
                Email = token.User.Email
            };
        }
    }
}


using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler
        : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _hasher;

        public ResetPasswordCommandHandler(
            IApplicationDbContext context,
            IPasswordHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public async Task<Unit> Handle(
            ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var otp = await _context.PasswordResetOtps
                .FirstOrDefaultAsync(x =>
                    x.Email == request.Email &&
                    x.Otp == request.Otp &&
                    !x.IsUsed &&
                    x.ExpiresAt > DateTime.UtcNow);

            if (otp == null)
                throw new Exception("Invalid OTP");

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
                throw new Exception("User not found");

            user.PasswordHash = _hasher.Hash(request.NewPassword);

            otp.IsUsed = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
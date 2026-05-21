using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler
        : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(
            IApplicationDbContext context,
            IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(
            ForgotPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
                throw new Exception("User not found");

            var otp = new Random().Next(100000, 999999).ToString();

            var resetOtp = new PasswordResetOtp
            {
                Email = request.Email,
                Otp = otp,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false
            };

            _context.PasswordResetOtps.Add(resetOtp);

            await _context.SaveChangesAsync(cancellationToken);

            await _emailService.SendAsync(
                request.Email,
                "Password Reset OTP",
                $"Your OTP is: {otp}");

            return Unit.Value;
        }
    }
}
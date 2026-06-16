using LearnFlowERP.Application.Common.Exceptions;
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
                throw new NotFoundException("User not found");

            var otp = new Random().Next(100000, 999999).ToString();

            var resetOtp = new PasswordResetOtp
            {
                Email = request.Email,
                Otp = otp,
                ExpiresAt = DateTime.Now.AddMinutes(10),
                IsUsed = false
            };

            _context.PasswordResetOtps.Add(resetOtp);

            await _context.SaveChangesAsync(cancellationToken);

            await _emailService.SendAsync(
                request.Email,
                "Password Reset Verification Code",
                $@"
                <div style='font-family: Arial, sans-serif;'>
                    <h2>Password Reset Request</h2>
                    <p>We received a request to reset your password.</p>

                    <p>Your verification code is:</p>

                    <div style='
                        font-size: 28px;
                        font-weight: bold;
                        letter-spacing: 5px;
                        color: #2563eb;
                        margin: 20px 0;'>
                        {otp}
                    </div>

                    <p>This code will expire in <strong>10 minutes</strong>.</p>

                    <p>If you did not request a password reset, you can safely ignore this email.</p>

                    <p><strong>Never share this code with anyone.</strong></p>

                    <br />
                    <p>Regards,<br />LearnFlow ERP Team</p>
                </div>");

            return Unit.Value;
        }
    }
}
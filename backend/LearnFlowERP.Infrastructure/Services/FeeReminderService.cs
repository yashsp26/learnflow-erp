using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using LearnFlowERP.Domain.Enums;
using LearnFlowERP.Infrastructure.BackgroundJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearnFlowERP.Infrastructure.Services
{
    public class FeeReminderService
        : IFeeReminderService
    {
        private readonly IApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly INotificationService _notificationService;
        private readonly ILogger<FeeReminderScheduler> _logger;

        public FeeReminderService(
            IApplicationDbContext context,
            IEmailService emailService,
            INotificationService notificationService,
            ILogger<FeeReminderScheduler> logger)
        {
            _context = context;
            _emailService = emailService;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task SendReminderAsync(
            long feeId,
            CancellationToken cancellationToken = default)
        {
            var fee = await _context.Fees
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(
                    x => x.FeeId == feeId,
                    cancellationToken);

            if (fee == null)
                throw new Exception("Fee not found");

            if (fee.OutstandingAmount <= 0)
                return;

            var reminderSentToday =
                await _context.FeeReminders
                    .AnyAsync(
                        x =>
                            x.FeeId == fee.FeeId &&
                            x.Status == ReminderStatus.Pending &&
                            x.SentAt.Date == DateTime.Today,
                        cancellationToken);

                        if (reminderSentToday)
                            return;

            try
            {
                var email =
                    fee.Student.User?.Email;

                if (!string.IsNullOrWhiteSpace(email))
                {
                    _logger.LogInformation(
                        "Sending reminder to {Email}",
                        email);

                    await _emailService.SendAsync(
                        email,
                        "Fee Payment Reminder",
                        $"""
                        <p>Dear {fee.Student.FirstName},</p>

                        <p>We hope you are doing well.</p>

                        <p>This is a friendly reminder that there is an outstanding fee balance associated with your account. The details are as follows:</p>

                        <table style="border-collapse: collapse; margin: 15px 0;">
                            <tr>
                                <td style="padding: 8px; font-weight: bold;">Fee Type:</td>
                                <td style="padding: 8px;">{fee.FeeType}</td>
                            </tr>
                            <tr>
                                <td style="padding: 8px; font-weight: bold;">Outstanding Amount:</td>
                                <td style="padding: 8px;">₹{fee.OutstandingAmount:N2}</td>
                            </tr>
                            <tr>
                                <td style="padding: 8px; font-weight: bold;">Due Date:</td>
                                <td style="padding: 8px;">{fee.DueDate:dd-MMM-yyyy}</td>
                            </tr>
                        </table>

                        <p>We kindly request you to complete the payment at your earliest convenience.</p>

                        <p>If you have already made the payment, please disregard this reminder.</p>

                        <p>
                            Kind regards,<br/>
                            <strong>Accounts Department</strong><br/>
                            LearnFlow ERP
                        </p>
                        """);

                    _logger.LogInformation(
                        "Email sent to {Email}",
                        email);

                }

                if (fee.Student.UserId.HasValue)
                {
                    await _notificationService.SendAsync(
                        fee.Student.UserId.Value,
                        "Fee Payment Reminder",
                        $"Outstanding fee amount ₹{fee.OutstandingAmount:N2}",
                        NotificationModule.Finance,
                        fee.FeeId);
                }

                _context.FeeReminders.Add(
                    new FeeReminder
                    {
                        FeeId = fee.FeeId,
                        TenantId = fee.TenantId,
                        Channel = ReminderChannel.Email,
                        Status = ReminderStatus.Sent,
                        SentAt = DateTime.Now
                    });

                await _context.SaveChangesAsync(
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _context.FeeReminders.Add(
                    new FeeReminder
                    {
                        FeeId = fee.FeeId,
                        TenantId = fee.TenantId,
                        Channel = ReminderChannel.Email,
                        Status = ReminderStatus.Failed,
                        FailureReason = ex.Message,
                        SentAt = DateTime.Now
                    });

                await _context.SaveChangesAsync(
                    cancellationToken);

                throw;
            }
        }
    }
}
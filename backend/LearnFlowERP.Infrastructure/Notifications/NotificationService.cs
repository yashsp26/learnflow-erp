using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using LearnFlowERP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearnFlowERP.Infrastructure.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IApplicationDbContext _context;
        private readonly IFcmService _fcmService;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            IApplicationDbContext context,
            IFcmService fcmService,
            ICurrentUserService currentUser,
            ILogger<NotificationService> logger)
        {
            _context = context;
            _fcmService = fcmService;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task SendAsync(
            long userId,
            string title,
            string message,
            NotificationModule module,
            long? referenceId = null)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstAsync(x => x.UserId == userId);
            var notification = new Notification
            {
                TenantId = user.TenantId,
                Title = title,
                Message = message,
                Module = module,
                Type = NotificationType.Info,
                ReferenceId = referenceId
            };

            notification.UserNotifications.Add(
                new UserNotification
                {
                    UserId = userId,
                    IsRead = false
                });

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            var tokens = await _context.UserDevices
                .Where(x =>
                    x.UserId == userId &&
                    x.IsActive)
                .Select(x => x.DeviceToken)
                .ToListAsync();

            if (!tokens.Any())
                return;

            try
            {
                await _fcmService.SendManyAsync(
                    tokens,
                    title,
                    message);

                _logger.LogInformation(
                    "FCM notification sent to UserId {UserId}. Device Count: {Count}",
                    userId,
                    tokens.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "FCM notification failed for UserId {UserId}",
                    userId);
            }
        }

        public async Task SendToStudentAsync(
            long studentId,
            string title,
            string message,
            NotificationModule module,
            long? referenceId = null)
        {
            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.StudentId == studentId);

            if (student?.UserId == null)
            {
                _logger.LogWarning(
                    "Student {StudentId} has no linked user account",
                    studentId);

                return;
            }

            await SendAsync(
                student.UserId.Value,
                title,
                message,
                module,
                referenceId);
        }
    }
}
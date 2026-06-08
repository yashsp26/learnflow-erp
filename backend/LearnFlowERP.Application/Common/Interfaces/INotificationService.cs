using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(
            long userId,
            string title,
            string message,
            NotificationModule module,
            long? referenceId = null);

        Task SendToStudentAsync(
            long studentId,
            string title,
            string message,
            NotificationModule module,
            long? referenceId = null);
    }
}
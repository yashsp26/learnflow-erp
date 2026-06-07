namespace LearnFlowERP.Application.Common.Interfaces
{
    public interface IFeeReminderService
    {
        Task SendReminderAsync(
            long feeId,
            CancellationToken cancellationToken = default);
    }
}
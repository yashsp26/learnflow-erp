
namespace LearnFlowERP.Application.Common.Interfaces
{
    public interface IFcmService
    {
        Task SendAsync(
            string deviceToken,
            string title,
            string body);

        Task SendManyAsync(
            IEnumerable<string> deviceTokens,
            string title,
            string body);
    }
}

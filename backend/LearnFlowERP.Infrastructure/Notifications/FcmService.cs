using LearnFlowERP.Application.Common.Interfaces;
using FirebaseAdmin.Messaging;
using FirebaseNotification = FirebaseAdmin.Messaging.Notification;

namespace LearnFlowERP.Infrastructure.Notifications
{
    public class FcmService : IFcmService
    {
        public async Task SendAsync(
            string deviceToken,
            string title,
            string body)
        {
            var message = new Message
            {
                Token = deviceToken,

                Notification =
                    new FirebaseNotification
                    {
                        Title = title,
                        Body = body
                    }
            };

            await FirebaseMessaging
                .DefaultInstance
                .SendAsync(message);
        }

        public async Task SendManyAsync(
           IEnumerable<string> deviceTokens,
           string title,
           string body)
        {
            var tasks = deviceTokens
                .Select(token =>
                    SendAsync(
                        token,
                        title,
                        body));

            await Task.WhenAll(tasks);
        }
    }
}

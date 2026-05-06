using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Users.Commands.Events;
using MediatR;

namespace LearnFlowERP.Application.Features.Users.EventHandlers
{
    public class UserCreatedEventHandler
    : INotificationHandler<UserCreatedEvent>
    {
        private readonly IEmailService _emailService;

        public UserCreatedEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(
            UserCreatedEvent notification,
            CancellationToken cancellationToken)
        {
            await _emailService.SendAsync(
                notification.Email,
                "Welcome to LearnFlow ERP",
                $"<h3>Hello {notification.Username}</h3><p>Your account is created.</p>"
            );
        }
    }
}

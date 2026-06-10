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
                $"""
               <div style="font-family: Arial, sans-serif; line-height: 1.6;">
                   <h2 style="color:#2563eb;">Welcome to LearnFlow ERP</h2>

                   <p>Hello <strong>{notification.Username}</strong>,</p>

                   <p>Your account has been successfully created and is now ready to use.</p>

                   <p>
                       You can log in to access your dashboard, manage your profile,
                       and start using the platform's features.
                   </p>

                   <p>We're excited to have you on board!</p>

                   <p>
                       Regards,<br/>
                       <strong>LearnFlow ERP Team</strong>
                   </p>
               </div>
               """
            );
        }
    }
}

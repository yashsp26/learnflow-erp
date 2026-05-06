using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LearnFlowERP.Application.Features.Users.Commands.Events
{
    public record UserCreatedEvent(string Email, string Username) : INotification;
}

using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Notifications.Commands.CreateNotification;
using LearnFlowERP.Application.Features.Notifications.Commands.MarkAllAsRead;
using LearnFlowERP.Application.Features.Notifications.Commands.MarkAsRead;
using LearnFlowERP.Application.Features.Notifications.Queries.GetNotifications;
using LearnFlowERP.Application.Features.Notifications.Queries.GetUnreadNotifications;
using LearnFlowERP.Application.Features.Notifications.Queries.GetUnreadNotificationCount;
using LearnFlowERP.Application.Features.FeeReminders.Commands.SendAllFeeReminders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("CreateNotification")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateNotificationCommand command)
        {
            var id =
                await _mediator.Send(command);

            return Ok(new { notificationId = id });
        }

        [Permission("ViewNotification")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(
                await _mediator.Send(
                    new GetNotificationsQuery()));
        }

        [Permission("ViewNotification")]
        [HttpGet("unread")]
        public async Task<IActionResult> GetUnread()
        {
            return Ok(
                await _mediator.Send(
                    new GetUnreadNotificationsQuery()));
        }

        [Permission("ManageNotification")]
        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkRead(
            long id)
        {
            await _mediator.Send(
                new MarkAsReadCommand(id));

            return Ok(new { updated = true });
        }

        [Permission("ManageNotification")]
        [HttpPatch("read-all")]
        public async Task<IActionResult> MarkAllRead()
        {
            await _mediator.Send(
                new MarkAllAsReadCommand());

            return Ok(new { updated = true });
        }

        [Permission("ViewNotification")]
        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var count = await _mediator.Send(
                new GetUnreadNotificationCountQuery());

            return Ok(new
            {
                unreadCount = count
            });
        }

        [Permission("SendFeeReminder")]
        [HttpPost("send-all")]
        public async Task<IActionResult> SendAll()
        {
            var count = await _mediator.Send(
                new SendAllFeeRemindersCommand());

            return Ok(new
            {
                remindersSent = count
            });
        }
    }
}
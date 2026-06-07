using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.FeeReminders.Commands.SendFeeReminder;
using LearnFlowERP.Application.Features.FeeReminders.Queries.GetFeeReminderHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/fee-reminders")]
    public class FeeRemindersController
        : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeeRemindersController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("SendFeeReminder")]
        [HttpPost("send/{feeId}")]
        public async Task<IActionResult> Send(
            long feeId)
        {
            await _mediator.Send(
                new SendFeeReminderCommand(feeId));

            return Ok(new
            {
                sent = true
            });
        }

        [Permission("ViewFeeReminder")]
        [HttpGet("history")]
        public async Task<IActionResult> History()
        {
            return Ok(
                await _mediator.Send(
                    new GetFeeReminderHistoryQuery()));
        }
    }
}
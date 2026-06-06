using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Refunds.Commands.CreateRefund;
using LearnFlowERP.Application.Features.Refunds.Queries.GetPaymentRefunds;
using LearnFlowERP.Application.Features.Refunds.Queries.GetRefundById;
using LearnFlowERP.Application.Features.Refunds.Queries.GetRefunds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/refunds")]
    public class RefundsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RefundsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("CreateRefund")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateRefundCommand command)
        {
            var refundId =
                await _mediator.Send(command);

            return Ok(new
            {
                refundId
            });
        }

        [Permission("ViewRefund")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _mediator.Send(
                    new GetRefundsQuery()));
        }

        [Permission("ViewRefund")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(
                await _mediator.Send(
                    new GetRefundByIdQuery(id)));
        }

        [Permission("ViewRefund")]
        [HttpGet("payment/{paymentId}")]
        public async Task<IActionResult> GetPaymentRefunds(
            long paymentId)
        {
            return Ok(
                await _mediator.Send(
                    new GetPaymentRefundsQuery(paymentId)));
        }
    }
}
using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Payments.Commands.CreatePayment;
using LearnFlowERP.Application.Features.Payments.Queries.GetPaymentById;
using LearnFlowERP.Application.Features.Payments.Queries.GetStudentPayments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("CreatePayment")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreatePaymentCommand command)
        {
            var paymentId =
                await _mediator.Send(command);

            return Ok(new
            {
                paymentId
            });
        }

        [Permission("ViewPayment")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(
                await _mediator.Send(
                    new GetPaymentByIdQuery(id)));
        }

        [Permission("ViewPayment")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentPayments(
            long studentId)
        {
            return Ok(
                await _mediator.Send(
                    new GetStudentPaymentsQuery(studentId)));
        }
    }
}
using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Payments.Commands.CreatePayment;
using LearnFlowERP.Application.Features.Payments.Queries.GetPaymentById;
using LearnFlowERP.Application.Features.Payments.Queries.GetReceiptByPaymentId;
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
        private readonly IReceiptPdfService _pdfService;
        public PaymentsController(IMediator mediator, IReceiptPdfService pdfService)
        {
            _mediator = mediator;
            _pdfService = pdfService;
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

        [Permission("ViewPayment")]
        [HttpGet("{paymentId}/receipt/pdf")]
        public async Task<IActionResult> ReceiptPdf(
    long paymentId)
        {
            var receipt =
                await _mediator.Send(
                    new GetReceiptByPaymentIdQuery(paymentId));

            var pdf =
                _pdfService.GenerateReceiptPdf(receipt);

            return File(
                pdf,
                "application/pdf",
                $"Receipt-{receipt.ReceiptNumber}.pdf");
        }


        [Permission("ViewPayment")]
        [HttpGet("{paymentId}/receipt")]
        public async Task<IActionResult> Receipt(long paymentId)
        {
            return Ok(
                await _mediator.Send(
                    new GetReceiptByPaymentIdQuery(paymentId)));
        }
    }
}
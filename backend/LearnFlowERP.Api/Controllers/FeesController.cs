using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Fees.Commands.CreateFee;
using LearnFlowERP.Application.Features.Fees.Queries.GetFeeById;
using LearnFlowERP.Application.Features.Fees.Queries.GetFees;
using LearnFlowERP.Application.Features.Fees.Queries.GetStudentFees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/fees")]
    public class FeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("CreateFee")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateFeeCommand command)
        {
            var feeId =
                await _mediator.Send(command);

            return Ok(new
            {
                feeId
            });
        }

        [Permission("ViewFee")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(
                await _mediator.Send(
                    new GetFeeByIdQuery(id)));
        }

        [Permission("ViewFee")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _mediator.Send(
                    new GetFeesQuery()));
        }

        [Permission("ViewFee")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentFees(
            long studentId)
        {
            return Ok(
                await _mediator.Send(
                    new GetStudentFeesQuery(studentId)));
        }
    }
}
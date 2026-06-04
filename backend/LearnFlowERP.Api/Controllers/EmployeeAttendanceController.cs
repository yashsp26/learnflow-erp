using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.EmployeeAttendances.Commands.MarkEmployeeAttendance;
using LearnFlowERP.Application.Features.EmployeeAttendances.Queries.GetEmployeeAttendance;
using LearnFlowERP.Application.Features.EmployeeAttendances.Queries.GetTodayEmployeeAttendance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/employee-attendance")]
    public class EmployeeAttendanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeAttendanceController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("MarkEmployeeAttendance")]
        [HttpPost("mark")]
        public async Task<IActionResult> Mark(
            MarkEmployeeAttendanceCommand command)
        {
            await _mediator.Send(command);

            return Ok(new
            {
                marked = true
            });
        }

        [Permission("ViewEmployeeAttendance")]
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> Get(
            long employeeId)
        {
            return Ok(
                await _mediator.Send(
                    new GetEmployeeAttendanceQuery(
                        employeeId)));
        }

        [Permission("ViewEmployeeAttendance")]
        [HttpGet("today")]
        public async Task<IActionResult> Today()
        {
            return Ok(
                await _mediator.Send(
                    new GetTodayEmployeeAttendanceQuery()));
        }
    }
}
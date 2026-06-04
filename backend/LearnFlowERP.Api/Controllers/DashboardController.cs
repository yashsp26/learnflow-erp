using LearnFlowERP.Application.Features.Dashboard.Queries.GetAdminDashboard;
using LearnFlowERP.Application.Features.Dashboard.Queries.GetTeacherDashboard;
using LearnFlowERP.Application.Features.Dashboard.Queries.GetStudentDashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("admin")]
        public async Task<IActionResult> Admin()
        {
            return Ok(
                await _mediator.Send(
                    new GetAdminDashboardQuery()));
        }

        [HttpGet("teacher")]
        public async Task<IActionResult> Teacher()
        {
            return Ok(
                await _mediator.Send(
                    new GetTeacherDashboardQuery()));
        }

        [HttpGet("student")]
        public async Task<IActionResult> Student()
        {
            return Ok(
                await _mediator.Send(
                    new GetStudentDashboardQuery()));
        }
    }
}
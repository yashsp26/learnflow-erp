using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Common.Models;
using LearnFlowERP.Application.Features.Employees.Commands.DeleteEmployee;
using LearnFlowERP.Application.Features.Employees.Commands.UpdateEmployeeDocument;
using LearnFlowERP.Application.Features.Employees.Commands.UpdateMyEmployeeProfile;
using LearnFlowERP.Application.Features.Employees.Queries.GetEmployeeById;
using LearnFlowERP.Application.Features.Employees.Queries.GetEmployees;
using LearnFlowERP.Application.Features.Employees.Queries.GetMyEmployeeProfile;
using LearnFlowERP.Application.Features.Onboarding.Commands.Employees.CompleteEmployeeProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ==========================================
        // ONBOARDING
        // ==========================================

        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfile(
            CompleteEmployeeProfileCommand command)
        {
            await _mediator.Send(command);

            return Ok(new
            {
                completed = true
            });
        }

        // ==========================================
        // SELF PROFILE
        // ==========================================

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var result = await _mediator.Send(
                new GetMyEmployeeProfileQuery());

            return Ok(result);
        }

        [HttpPatch("me")]
        public async Task<IActionResult> UpdateMyProfile(
            UpdateMyEmployeeProfileCommand command)
        {
            await _mediator.Send(command);

            return Ok(new
            {
                updated = true
            });
        }

        // ==========================================
        // DOCUMENTS
        // ==========================================

        [Permission("UpdateEmployee")]
        [HttpPatch("{id}/document")]
        public async Task<IActionResult> UpdateDocument(
            long id,
            [FromBody] FileUrlDto dto)
        {
            await _mediator.Send(
                new UpdateEmployeeDocumentCommand(
                    id,
                    dto.Url));

            return Ok(new
            {
                updated = true
            });
        }

        // ==========================================
        // ADMIN APIS
        // ==========================================

        [Permission("ViewEmployee")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees(
            [FromQuery] GetEmployeesQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Permission("ViewEmployee")]
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetEmployee(
            long id)
        {
            var result = await _mediator.Send(
                new GetEmployeeByIdQuery(id));

            return Ok(result);
        }

        [Permission("DeleteEmployee")]
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteEmployee(
            long id)
        {
            await _mediator.Send(
                new DeleteEmployeeCommand(id));

            return Ok(new
            {
                deleted = true
            });
        }
    }
}
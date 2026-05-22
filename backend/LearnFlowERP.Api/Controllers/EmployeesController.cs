using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Employees.Commands.UpdateEmployeeDocument;
using LearnFlowERP.Application.Features.Onboarding.Commands.Employees.CompleteEmployeeProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LearnFlowERP.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfile(CompleteEmployeeProfileCommand command)
        {
            await _mediator.Send(command);

            return Ok(new
            {
                completed = true
            });
        }

        [Permission("UpdateEmployee")]
        [HttpPatch("{id}/document")]
        public async Task<IActionResult> UpdateDocument(long id, [FromBody] FileUrlDto dto)
        {
            await _mediator.Send(
                new UpdateEmployeeDocumentCommand(id, dto.Url));

            return Ok(new { updated = true });
        }
    }
}   

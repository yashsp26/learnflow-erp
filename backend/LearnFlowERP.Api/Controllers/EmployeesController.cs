using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Employees.Commands.UpdateEmployeeDocument;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LearnFlowERP.Application.Common.Models;

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

        [Permission("UpdateEmployee")]
        [HttpPatch("{id}/document")]
        public async Task<IActionResult> UpdateDocument(long id, [FromBody] FileUrlDto dto)
        {
            await _mediator.Send(
                new UpdateEmployeeDocumentCommand(id, dto.Url));

            return Ok(Url);
        }
    }
}   

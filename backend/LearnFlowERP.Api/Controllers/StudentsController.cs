using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Common.Models;
using LearnFlowERP.Application.Features.Students.Commands.UpdateStudentDocument;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Produces("application/json")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("UpdateStudent")]
        [HttpPatch("{id}/document")]
        public async Task<IActionResult> UpdateDocument(long id, [FromBody] FileUrlDto dto)
        {
            await _mediator.Send(new UpdateStudentDocumentCommand(id, dto.Url));
            return Ok(Url);
        }
    }
}

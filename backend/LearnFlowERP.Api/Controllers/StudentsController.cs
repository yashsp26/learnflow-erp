using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Common.Models;
using LearnFlowERP.Application.Features.Onboarding.Students.Commands.CompleteStudentProfile;
using LearnFlowERP.Application.Features.Students.Commands.DeleteStudent;
using LearnFlowERP.Application.Features.Students.Commands.UpdateMyStudentProfile;
using LearnFlowERP.Application.Features.Students.Commands.UpdateStudentDocument;
using LearnFlowERP.Application.Features.Students.Queries.GetMyStudentProfile;
using LearnFlowERP.Application.Features.Students.Queries.GetStudentById;
using LearnFlowERP.Application.Features.Students.Queries.GetStudents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        // ==========================================
        // ONBOARDING
        // ==========================================


        [HttpPost("student-profile")]
        public async Task<IActionResult> CompleteStudentProfile(
            CompleteStudentProfileCommand command)
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
                new GetMyStudentProfileQuery());

            return Ok(result);
        }

        [HttpPatch("me")]
        public async Task<IActionResult> UpdateMyProfile(
            UpdateMyStudentProfileCommand command)
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

        [Permission("UpdateStudent")]
        [HttpPatch("{id}/document")]
        public async Task<IActionResult> UpdateDocument(
            long id,
            [FromBody] FileUrlDto dto)
        {
            await _mediator.Send(
                new UpdateStudentDocumentCommand(
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

        [Permission("ViewStudent")]
        [HttpGet]
        public async Task<IActionResult> GetStudents(
            [FromQuery] GetStudentsQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Permission("ViewStudent")]
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetStudent(
            long id)
        {
            var result = await _mediator.Send(
                new GetStudentByIdQuery(id));

            return Ok(result);
        }

        [Permission("DeleteStudent")]
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteStudent(
            long id)
        {
            await _mediator.Send(
                new DeleteStudentCommand(id));

            return Ok(new
            {
                deleted = true
            });
        }
    }
}
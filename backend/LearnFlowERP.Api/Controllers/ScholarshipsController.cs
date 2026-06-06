using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Scholarships.Commands.CreateScholarship;
using LearnFlowERP.Application.Features.Scholarships.Commands.RemoveScholarship;
using LearnFlowERP.Application.Features.Scholarships.Queries.GetScholarshipById;
using LearnFlowERP.Application.Features.Scholarships.Queries.GetStudentScholarships;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/scholarships")]
    public class ScholarshipsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScholarshipsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("ManageScholarship")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateScholarshipCommand command)
        {
            var id = await _mediator.Send(command);

            return Ok(new
            {
                scholarshipId = id
            });
        }

        [Permission("ManageScholarship")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            await _mediator.Send(
                new RemoveScholarshipCommand(id));

            return Ok(new
            {
                removed = true
            });
        }

        [Permission("ViewScholarship")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(
                await _mediator.Send(
                    new GetScholarshipByIdQuery(id)));
        }

        [Permission("ViewScholarship")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentScholarships(
            long studentId)
        {
            return Ok(
                await _mediator.Send(
                    new GetStudentScholarshipsQuery(studentId)));
        }
    }
}
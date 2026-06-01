using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Courses.Commands.CreateCourse;
using LearnFlowERP.Application.Features.Courses.Commands.DeleteCourse;
using LearnFlowERP.Application.Features.Courses.Commands.UpdateCourse;
using LearnFlowERP.Application.Features.Courses.Queries.GetCourseById;
using LearnFlowERP.Application.Features.Courses.Queries.GetCourses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("CreateCourse")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCourseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Permission("ViewCourse")]
        [HttpGet]
        public async Task<IActionResult> GetCourses(
            [FromQuery] GetCoursesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Permission("ViewCourse")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(
            long id)
        {
            return Ok(
                await _mediator.Send(
                    new GetCourseByIdQuery(id)));
        }

        [Permission("UpdateCourse")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(
            long id,
            UpdateCourseCommand command)
        {
            command.CourseId = id;

            await _mediator.Send(command);

            return Ok();
        }

        [Permission("DeleteCourse")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            long id)
        {
            await _mediator.Send(
                new DeleteCourseCommand(id));

            return Ok();
        }
    }
}
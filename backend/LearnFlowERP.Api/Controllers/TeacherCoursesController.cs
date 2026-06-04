using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.TeacherCourses.Commands.AssignTeacherCourse;
using LearnFlowERP.Application.Features.TeacherCourses.Commands.UnassignTeacherCourse;
using LearnFlowERP.Application.Features.TeacherCourses.Queries.GetCourseTeachers;
using LearnFlowERP.Application.Features.TeacherCourses.Queries.GetTeacherCourses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/teacher-courses")]
    public class TeacherCoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeacherCoursesController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("AssignTeacherCourse")]
        [HttpPost("assign")]
        public async Task<IActionResult> Assign(
            AssignTeacherCourseCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [Permission("AssignTeacherCourse")]
        [HttpDelete("unassign")]
        public async Task<IActionResult> Unassign(
            UnassignTeacherCourseCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [Permission("ViewTeacherCourse")]
        [HttpGet("teacher/{employeeId}")]
        public async Task<IActionResult> TeacherCourses(
            long employeeId)
        {
            return Ok(
                await _mediator.Send(
                    new GetTeacherCoursesQuery(employeeId)));
        }

        [Permission("ViewTeacherCourse")]
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> CourseTeachers(
            long courseId)
        {
            return Ok(
                await _mediator.Send(
                    new GetCourseTeachersQuery(courseId)));
        }
    }
}
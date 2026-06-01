using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.StudentCourses.Commands.AssignStudentToCourse;
using LearnFlowERP.Application.Features.StudentCourses.Commands.RemoveStudentFromCourse;
using LearnFlowERP.Application.Features.StudentCourses.Queries.GetCourseStudents;
using LearnFlowERP.Application.Features.StudentCourses.Queries.GetStudentCourses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/student-courses")]
    public class StudentCoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentCoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("AssignCourse")]
        [HttpPost("assign")]
        public async Task<IActionResult> Assign(
            AssignStudentToCourseCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [Permission("AssignCourse")]
        [HttpDelete("remove")]
        public async Task<IActionResult> Remove(
            RemoveStudentFromCourseCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [Permission("ViewStudent")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> StudentCourses(
            long studentId)
        {
            return Ok(
                await _mediator.Send(
                    new GetStudentCoursesQuery(studentId)));
        }

        [Permission("ViewCourse")]
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> CourseStudents(
            long courseId)
        {
            return Ok(
                await _mediator.Send(
                    new GetCourseStudentsQuery(courseId)));
        }
    }
}
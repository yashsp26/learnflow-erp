using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.StudentAttendances.Commands.MarkStudentAttendance;
using LearnFlowERP.Application.Features.StudentAttendances.Queries.GetCourseAttendance;
using LearnFlowERP.Application.Features.StudentAttendances.Queries.GetStudentAttendance;
using LearnFlowERP.Application.Features.StudentAttendances.Queries.GetTodayAttendance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/student-attendance")]
    public class StudentAttendanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentAttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("MarkStudentAttendance")]
        [HttpPost("mark")]
        public async Task<IActionResult> Mark(
            MarkStudentAttendanceCommand command)
        {
            await _mediator.Send(command);

            return Ok(new { marked = true });
        }

        [Permission("ViewStudentAttendance")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> Student(long studentId)
        {
            return Ok(await _mediator.Send(
                new GetStudentAttendanceQuery(studentId)));
        }

        [Permission("ViewStudentAttendance")]
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> Course(long courseId)
        {
            return Ok(await _mediator.Send(
                new GetCourseAttendanceQuery(courseId)));
        }

        [Permission("ViewStudentAttendance")]
        [HttpGet("today")]
        public async Task<IActionResult> Today()
        {
            return Ok(await _mediator.Send(
                new GetTodayAttendanceQuery()));
        }
    }
}
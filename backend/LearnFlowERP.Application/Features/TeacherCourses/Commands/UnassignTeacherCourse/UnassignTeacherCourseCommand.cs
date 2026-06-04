using MediatR;

namespace LearnFlowERP.Application.Features.TeacherCourses.Commands.UnassignTeacherCourse
{
    public class UnassignTeacherCourseCommand
        : IRequest<Unit>
    {
        public long EmployeeId { get; set; }

        public long CourseId { get; set; }
    }
}
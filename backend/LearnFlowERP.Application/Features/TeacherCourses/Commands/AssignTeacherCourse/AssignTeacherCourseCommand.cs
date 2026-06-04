using MediatR;

namespace LearnFlowERP.Application.Features.TeacherCourses.Commands.AssignTeacherCourse
{
    public class AssignTeacherCourseCommand : IRequest<Unit>
    {
        public long EmployeeId { get; set; }

        public long CourseId { get; set; }
    }
}
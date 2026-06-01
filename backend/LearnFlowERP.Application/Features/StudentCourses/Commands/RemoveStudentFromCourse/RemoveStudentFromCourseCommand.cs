using MediatR;

namespace LearnFlowERP.Application.Features.StudentCourses.Commands.RemoveStudentFromCourse
{
    public class RemoveStudentFromCourseCommand : IRequest<Unit>
    {
        public long StudentId { get; set; }

        public long CourseId { get; set; }
    }
}
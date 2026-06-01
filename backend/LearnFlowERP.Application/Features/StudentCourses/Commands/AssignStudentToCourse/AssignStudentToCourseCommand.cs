using MediatR;

namespace LearnFlowERP.Application.Features.StudentCourses.Commands.AssignStudentToCourse
{
    public class AssignStudentToCourseCommand : IRequest<Unit>
    {
        public long StudentId { get; set; }

        public long CourseId { get; set; }
    }
}
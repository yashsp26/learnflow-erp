using MediatR;

namespace LearnFlowERP.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<Unit>
    {
        public long CourseId { get; set; }

        public string CourseCode { get; set; } = null!;

        public string CourseName { get; set; } = null!;

        public int Credits { get; set; }
    }
}
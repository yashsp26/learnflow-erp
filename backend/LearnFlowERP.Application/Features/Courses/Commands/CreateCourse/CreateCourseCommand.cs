using MediatR;

namespace LearnFlowERP.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<long>
    {
        public string CourseCode { get; set; } = null!;

        public string CourseName { get; set; } = null!;

        public int Credits { get; set; }
    }
}
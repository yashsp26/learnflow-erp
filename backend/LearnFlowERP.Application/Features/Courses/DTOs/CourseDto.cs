namespace LearnFlowERP.Application.Features.Courses.DTOs
{
    public class CourseDto
    {
        public long CourseId { get; set; }

        public string CourseCode { get; set; } = null!;

        public string CourseName { get; set; } = null!;

        public int Credits { get; set; }
    }
}
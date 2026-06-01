namespace LearnFlowERP.Application.Features.StudentCourses.DTOs
{
    public class StudentCourseDto
    {
        public long StudentId { get; set; }

        public long CourseId { get; set; }

        public string StudentName { get; set; } = null!;

        public string CourseName { get; set; } = null!;

        public DateTime? EnrollmentDate { get; set; }

        public string? Status { get; set; }
    }
}
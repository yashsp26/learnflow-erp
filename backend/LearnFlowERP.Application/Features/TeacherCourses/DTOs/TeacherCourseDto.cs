namespace LearnFlowERP.Application.Features.TeacherCourses.DTOs
{
    public class TeacherCourseDto
    {
        public long EmployeeId { get; set; }

        public long CourseId { get; set; }

        public string EmployeeName { get; set; } = null!;

        public string EmpCode { get; set; } = null!;

        public string CourseCode { get; set; } = null!;

        public string CourseName { get; set; } = null!;

        public DateTime AssignedAt { get; set; }
    }
}
namespace LearnFlowERP.Domain.Entities
{
    public class TeacherCourse
    {
        public long EmployeeId { get; set; }

        public long CourseId { get; set; }

        public long TenantId { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.Now;

        public Employee Employee { get; set; } = null!;

        public Course Course { get; set; } = null!;
    }
}
using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Domain.Entities
{
    public class StudentAttendance
    {
        public long StudentAttendanceId { get; set; }

        public long StudentId { get; set; }

        public long CourseId { get; set; }

        public long MarkedByEmployeeId { get; set; }

        public long TenantId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public AttendanceStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Student Student { get; set; } = null!;

        public Course Course { get; set; } = null!;

        public Employee MarkedByEmployee { get; set; } = null!;
    }
}
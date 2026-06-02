using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Domain.Entities
{
    public class EmployeeAttendance
    {
        public long EmployeeAttendanceId { get; set; }

        public long EmployeeId { get; set; }

        public long TenantId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public AttendanceStatus Status { get; set; }

        public Employee Employee { get; set; } = null!;
    }
}
using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Application.Features.EmployeeAttendances.DTOs
{
    public class EmployeeAttendanceDto
    {
        public long EmployeeId { get; set; }

        public string EmployeeName { get; set; } = null!;

        public DateTime AttendanceDate { get; set; }

        public AttendanceStatus Status { get; set; }
    }
}
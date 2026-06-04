using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Application.Features.StudentAttendances.DTOs
{
    public class StudentAttendanceDto
    {
        public DateTime Date { get; set; }

        public AttendanceStatus Status { get; set; }

        public string CourseName { get; set; } = null!;
    }
}
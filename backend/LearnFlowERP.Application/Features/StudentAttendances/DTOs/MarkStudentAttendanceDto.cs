using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Application.Features.StudentAttendances.DTOs
{
    public class MarkStudentAttendanceDto
    {
        public long StudentId { get; set; }

        public AttendanceStatus Status { get; set; }
    }
}
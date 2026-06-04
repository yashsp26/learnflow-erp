namespace LearnFlowERP.Application.Features.Dashboard.DTOs
{
    public class StudentDashboardDto
    {
        public int TotalCourses { get; set; }

        public decimal AttendancePercentage { get; set; }

        public decimal PendingFees { get; set; }
    }
}
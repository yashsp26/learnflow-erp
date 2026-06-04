namespace LearnFlowERP.Application.Features.Dashboard.DTOs
{
    public class AdminDashboardDto
    {
        public int TotalStudents { get; set; }

        public int TotalEmployees { get; set; }

        public int TotalCourses { get; set; }

        public int TotalTeachers { get; set; }

        public int StudentsPresentToday { get; set; }

        public int EmployeesPresentToday { get; set; }

        public decimal TotalFees { get; set; }

        public decimal TotalCollected { get; set; }

        public decimal PendingFees { get; set; }
    }
}
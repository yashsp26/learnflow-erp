using System;

namespace LearnFlowERP.Application.Features.Employees.DTOs
{
    public class EmployeeDto
    {
        public long EmployeeId { get; set; }

        public long? UserId { get; set; }

        public string EmpCode { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Department { get; set; }

        public decimal? Salary { get; set; }

        public string? Email { get; set; }

        public string? DocumentUrl { get; set; }
    }
}

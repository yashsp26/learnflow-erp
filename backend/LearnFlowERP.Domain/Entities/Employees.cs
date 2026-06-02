using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public long EmployeeId { get; set; }

        public long? UserId { get; set; }

        public string EmpCode { get; set; } = null!;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Department { get; set; }
        public decimal? Salary { get; set; }
        public string? DocumentUrl { get; set; }
        public long DesignationId { get; set; }

        // Navigation
        public Designation Designation { get; set; } = null!;
        public Tenant Tenant { get; set; } = null!;
        public User? User { get; set; }

        public ICollection<TeacherCourse> TeacherCourses { get; set; }
            = new List<TeacherCourse>();
        public ICollection<EmployeeAttendance> Attendances { get; set; }
            = new List<EmployeeAttendance>();
        public ICollection<StudentAttendance> MarkedAttendances { get; set; }
            = new List<StudentAttendance>();
    }
}

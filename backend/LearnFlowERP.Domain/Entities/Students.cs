using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class Student : BaseEntity
    {
        public long StudentId { get; set; }

        public long? UserId { get; set; }

        public string EnrollmentNo { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime? Dob { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string? DocumentUrl { get; set; }

        // Navigation
        public Tenant Tenant { get; set; } = null!;
        public User? User { get; set; }
        public ICollection<StudentAttendance> Attendances { get; set; }
            = new List<StudentAttendance>(); 
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<Fee> Fees { get; set; } = new List<Fee>();
    }
}

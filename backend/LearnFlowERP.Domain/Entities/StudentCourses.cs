using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class StudentCourse
    {
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public long TenantId { get; set; }

        public DateTime? EnrollmentDate { get; set; }
        public string? Status { get; set; }

        // Navigation
        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}

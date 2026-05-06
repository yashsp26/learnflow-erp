using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class Course : BaseEntity
    {
        public long CourseId { get; set; }

        public string CourseCode { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public int Credits { get; set; }

        // Navigation
        public Tenant Tenant { get; set; } = null!;
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}

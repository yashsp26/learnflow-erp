using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class Tenant
    {
        public long TenantId { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        // Keep minimal navigation
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}

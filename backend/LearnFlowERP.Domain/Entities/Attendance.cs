using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class Attendance
    {
        public long AttendanceId { get; set; }

        public long EmployeeId { get; set; }
        public long TenantId { get; set; }

        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Employee Employee { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class Fee : BaseEntity
    {
        public long FeeId { get; set; }

        public long StudentId { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime? DueDate { get; set; }

        public string? Status { get; set; }

        // Navigation
        public Student Student { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}

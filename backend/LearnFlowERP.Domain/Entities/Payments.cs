using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class Payment
    {
        public long PaymentId { get; set; }

        public long FeeId { get; set; }
        public long TenantId { get; set; }

        public decimal AmountPaid { get; set; }

        public string? PaymentMethod { get; set; }
        public string? TransactionRef { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Fee Fee { get; set; } = null!;
    }
}

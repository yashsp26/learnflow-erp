using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class DesignationPermission
    {
        public long DesignationId { get; set; }

        public long PermissionId { get; set; }

        public Designation Designation { get; set; } = null!;

        public Permission Permission { get; set; } = null!;
    }
}

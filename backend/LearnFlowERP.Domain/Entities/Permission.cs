using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class Permission
    {
        public long PermissionId { get; set; }
        public string Name { get; set; } = null!;
    }
}

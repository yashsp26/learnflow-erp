using LearnFlowERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Application.Features.Permissions.DTOs
{
    public class RoleDto
    {
        public long RoleId { get; set; }

        public string RoleName { get; set; } = null!;

    }
}

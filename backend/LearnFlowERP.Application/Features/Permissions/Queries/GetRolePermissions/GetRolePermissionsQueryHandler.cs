using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetRolePermissions
{
    public class GetRolePermissionsQueryHandler
        : IRequestHandler<GetRolePermissionsQuery, List<string>>
    {
        private readonly IApplicationDbContext _context;

        public GetRolePermissionsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(
            GetRolePermissionsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.RolePermissions
                .Where(x => x.RoleId == request.RoleId)
                .Select(x => x.Permission.Name)
                .ToListAsync(cancellationToken);
        }
    }
}

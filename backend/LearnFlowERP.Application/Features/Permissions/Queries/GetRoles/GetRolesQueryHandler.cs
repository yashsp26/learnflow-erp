using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Permissions.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetRoles
{
    public class GetRolesQueryHandler
        : IRequestHandler<GetRolesQuery, List<RoleDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetRolesQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoleDto>> Handle(
            GetRolesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Where(x => x.IsActive)
                .OrderBy(x => x.RoleName)
                .Select(x => new RoleDto
                {
                    RoleId = x.RoleId,
                    RoleName = x.RoleName
                })
                .ToListAsync(cancellationToken);
        }
    }
}

using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Permissions.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetAllPermissions
{
    public class GetAllPermissionsQueryHandler
    : IRequestHandler<GetAllPermissionsQuery, List<PermissionDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllPermissionsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PermissionDto>> Handle(
            GetAllPermissionsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Permissions
                .Select(x => new PermissionDto
                {
                    PermissionId = x.PermissionId,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}

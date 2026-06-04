using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetDesignationPermissions
{
    public class GetDesignationPermissionsQueryHandler
        : IRequestHandler<GetDesignationPermissionsQuery, List<string>>
    {
        private readonly IApplicationDbContext _context;

        public GetDesignationPermissionsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(
            GetDesignationPermissionsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.DesignationPermissions
                .Where(x => x.DesignationId == request.DesignationId)
                .Select(x => x.Permission.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
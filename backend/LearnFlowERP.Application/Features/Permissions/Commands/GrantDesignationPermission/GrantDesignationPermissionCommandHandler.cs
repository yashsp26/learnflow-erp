using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace LearnFlowERP.Application.Features.Permissions.Commands.GrantDesignationPermission
{
    public class GrantDesignationPermissionCommandHandler
    : IRequestHandler<GrantDesignationPermissionCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPermissionCacheService _permissionCache;
        public GrantDesignationPermissionCommandHandler(
            IApplicationDbContext context, IPermissionCacheService permissionCache)
        {
            _context = context;
            _permissionCache = permissionCache;
        }

        public async Task<Unit> Handle(
            GrantDesignationPermissionCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _context.DesignationPermissions
                .AnyAsync(x =>
                    x.DesignationId == request.DesignationId &&
                    x.PermissionId == request.PermissionId,
                    cancellationToken);

            if (exists)
                return Unit.Value;

            _context.DesignationPermissions.Add(
                new DesignationPermission
                {
                    DesignationId = request.DesignationId,
                    PermissionId = request.PermissionId
                });

            await _context.SaveChangesAsync(cancellationToken);

            await _permissionCache
                .RemoveDesignationUsersPermissionsAsync(request.DesignationId);

            return Unit.Value;
        }
    }
}

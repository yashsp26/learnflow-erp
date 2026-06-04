using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Permissions.Commands.RevokeRolePermission
{
    public class RevokeRolePermissionCommandHandler
    : IRequestHandler<RevokeRolePermissionCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPermissionCacheService _permissionCache;
        public RevokeRolePermissionCommandHandler(
            IApplicationDbContext context, IPermissionCacheService permissionCache)
        {
            _context = context;
            _permissionCache = permissionCache;
        }

        public async Task<Unit> Handle(
            RevokeRolePermissionCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.RolePermissions
                .FirstOrDefaultAsync(x =>
                    x.RoleId == request.RoleId &&
                    x.PermissionId == request.PermissionId,
                    cancellationToken);

            if (entity == null)
                return Unit.Value;

            _context.RolePermissions.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _permissionCache
                .RemoveRoleUsersPermissionsAsync(request.RoleId);

            return Unit.Value;
        }
    }
}

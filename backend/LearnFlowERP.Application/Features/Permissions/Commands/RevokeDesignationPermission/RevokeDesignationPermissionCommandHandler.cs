using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Permissions.Commands.RevokeDesignationPermission
{
    public class RevokeDesignationPermissionCommandHandler
        : IRequestHandler<RevokeDesignationPermissionCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public RevokeDesignationPermissionCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            RevokeDesignationPermissionCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.DesignationPermissions
                .FirstOrDefaultAsync(
                    x =>
                        x.DesignationId == request.DesignationId &&
                        x.PermissionId == request.PermissionId,
                    cancellationToken);

            if (entity == null)
                return Unit.Value;

            _context.DesignationPermissions.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
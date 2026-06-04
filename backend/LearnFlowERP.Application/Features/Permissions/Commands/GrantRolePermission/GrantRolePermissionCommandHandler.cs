using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Application.Features.Permissions.Commands.GrantRolePermission
{
    public class GrantRolePermissionCommandHandler
    : IRequestHandler<GrantRolePermissionCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public GrantRolePermissionCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            GrantRolePermissionCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _context.RolePermissions
                .AnyAsync(x =>
                    x.RoleId == request.RoleId &&
                    x.PermissionId == request.PermissionId,
                    cancellationToken);

            if (exists)
                return Unit.Value;

            _context.RolePermissions.Add(
                new RolePermission
                {
                    RoleId = request.RoleId,
                    PermissionId = request.PermissionId
                });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

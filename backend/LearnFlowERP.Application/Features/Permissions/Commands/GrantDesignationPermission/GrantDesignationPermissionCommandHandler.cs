using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Application.Features.Permissions.Commands.GrantDesignationPermission
{
    public class GrantDesignationPermissionCommandHandler
    : IRequestHandler<GrantDesignationPermissionCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public GrantDesignationPermissionCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
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

            return Unit.Value;
        }
    }
}

using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Designations.Commands.CreateDesignation
{
    public class CreateDesignationCommandHandler
        : IRequestHandler<CreateDesignationCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateDesignationCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<long> Handle(
            CreateDesignationCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _context.Designations
                .AnyAsync(
                    x => x.Name == request.Name,
                    cancellationToken);

            if (exists)
                throw new DataAlreadyExistsException("Designation already exists");

            var designation = new Designation
            {
                Name = request.Name,
                TenantId = _currentUser.TenantId ?? 0,
                CreatedBy = _currentUser.UserId
            };

            _context.Designations.Add(designation);

            await _context.SaveChangesAsync(cancellationToken);

            return designation.DesignationId;
        }
    }
}
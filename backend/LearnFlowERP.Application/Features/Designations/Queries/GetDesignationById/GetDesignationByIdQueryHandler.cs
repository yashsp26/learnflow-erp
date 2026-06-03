using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Designations.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Designations.Queries.GetDesignationById
{
    public class GetDesignationByIdQueryHandler
        : IRequestHandler<GetDesignationByIdQuery, DesignationDto>
    {
        private readonly IApplicationDbContext _context;

        public GetDesignationByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DesignationDto> Handle(
            GetDesignationByIdQuery request,
            CancellationToken cancellationToken)
        {
            var designation = await _context.Designations
                .FirstOrDefaultAsync(
                    x => x.DesignationId == request.DesignationId &&
                         x.IsActive,
                    cancellationToken);

            if (designation == null)
                throw new Exception("Designation not found");

            return new DesignationDto
            {
                DesignationId = designation.DesignationId,
                Name = designation.Name
            };
        }
    }
}
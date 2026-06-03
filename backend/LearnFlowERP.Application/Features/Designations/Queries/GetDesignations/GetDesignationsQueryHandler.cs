using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Designations.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Designations.Queries.GetDesignations
{
    public class GetDesignationsQueryHandler
        : IRequestHandler<GetDesignationsQuery,
            List<DesignationDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetDesignationsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DesignationDto>> Handle(
            GetDesignationsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Designations
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new DesignationDto
                {
                    DesignationId = x.DesignationId,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}
using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Scholarships.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Scholarships.Queries.GetScholarshipById
{
    public class GetScholarshipByIdQueryHandler
        : IRequestHandler<GetScholarshipByIdQuery, ScholarshipDto>
    {
        private readonly IApplicationDbContext _context;

        public GetScholarshipByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ScholarshipDto> Handle(
            GetScholarshipByIdQuery request,
            CancellationToken cancellationToken)
        {
            var scholarship =
                await _context.StudentScholarships
                    .FirstOrDefaultAsync(
                        x => x.StudentScholarshipId ==
                             request.StudentScholarshipId,
                        cancellationToken);

            if (scholarship == null)
                throw new NotFoundException("Scholarship not found");

            return new ScholarshipDto
            {
                StudentScholarshipId =
                    scholarship.StudentScholarshipId,

                StudentId =
                    scholarship.StudentId,

                ScholarshipName =
                    scholarship.ScholarshipName,

                Amount =
                    scholarship.Amount,

                EffectiveFrom =
                    scholarship.EffectiveFrom,

                EffectiveTo =
                    scholarship.EffectiveTo,

                IsActive =
                    scholarship.IsActive
            };
        }
    }
}
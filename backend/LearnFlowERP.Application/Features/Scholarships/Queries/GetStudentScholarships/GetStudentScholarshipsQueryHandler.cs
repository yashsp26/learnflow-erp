using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Scholarships.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Scholarships.Queries.GetStudentScholarships
{
    public class GetStudentScholarshipsQueryHandler
        : IRequestHandler<
            GetStudentScholarshipsQuery,
            List<ScholarshipDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentScholarshipsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ScholarshipDto>> Handle(
            GetStudentScholarshipsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.StudentScholarships
                .Where(x => x.StudentId == request.StudentId)
                .Select(x => new ScholarshipDto
                {
                    StudentScholarshipId =
                        x.StudentScholarshipId,

                    StudentId =
                        x.StudentId,

                    ScholarshipName =
                        x.ScholarshipName,

                    Amount =
                        x.Amount,

                    EffectiveFrom =
                        x.EffectiveFrom,

                    EffectiveTo =
                        x.EffectiveTo,

                    IsActive =
                        x.IsActive
                })
                .ToListAsync(cancellationToken);
        }
    }
}
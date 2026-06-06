using LearnFlowERP.Application.Features.Scholarships.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Scholarships.Queries.GetStudentScholarships
{
    public record GetStudentScholarshipsQuery(
        long StudentId
    ) : IRequest<List<ScholarshipDto>>;
}
using LearnFlowERP.Application.Features.Finance.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetStudentLedger
{
    public record GetStudentLedgerQuery(long StudentId)
        : IRequest<List<StudentLedgerEntryDto>>;
}
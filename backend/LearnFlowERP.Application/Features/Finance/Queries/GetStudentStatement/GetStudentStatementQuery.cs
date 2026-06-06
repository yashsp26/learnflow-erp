using LearnFlowERP.Application.Features.Finance.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetStudentStatement
{
    public record GetStudentStatementQuery(long StudentId)
        : IRequest<StudentStatementDto>;
}
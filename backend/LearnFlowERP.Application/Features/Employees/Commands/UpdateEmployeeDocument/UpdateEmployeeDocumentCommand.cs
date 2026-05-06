using MediatR;
using Microsoft.AspNetCore.Http;

namespace LearnFlowERP.Application.Features.Employees.Commands.UpdateEmployeeDocument
{
    public record UpdateEmployeeDocumentCommand(
    long EmployeeId,
    string DocumentUrl   // 🔥 MUST EXIST
) : IRequest<Unit>;
}

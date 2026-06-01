using LearnFlowERP.Application.Features.Students.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Students.Queries.GetMyStudentProfile
{
    public record GetMyStudentProfileQuery()
        : IRequest<StudentDto>;
}
using LearnFlowERP.Application.Features.Students.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Students.Queries.GetStudentById
{
    public record GetStudentByIdQuery(long StudentId)
        : IRequest<StudentDto>;
}
using LearnFlowERP.Application.Common.Models;
using LearnFlowERP.Application.Features.Students.DTOs;
using MediatR;

public class GetStudentsQuery
    : IRequest<PagedResult<StudentDto>>
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }
}
using LearnFlowERP.Application.Common.Models;
using LearnFlowERP.Application.Features.Courses.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Courses.Queries.GetCourses
{
    public class GetCoursesQuery
        : IRequest<PagedResult<CourseDto>>
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }
    }
}
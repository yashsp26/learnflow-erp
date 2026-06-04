using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.StudentAttendances.Queries.GetTodayAttendance
{
    public class GetTodayAttendanceQueryHandler
         : IRequestHandler<GetTodayAttendanceQuery,
             List<StudentAttendance>>
    {
        private readonly IApplicationDbContext _context;

        public GetTodayAttendanceQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentAttendance>> Handle(
            GetTodayAttendanceQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.StudentAttendances
                .Where(x =>
                    x.AttendanceDate.Date ==
                    DateTime.Today)
                .ToListAsync(cancellationToken);
        }
    }
}

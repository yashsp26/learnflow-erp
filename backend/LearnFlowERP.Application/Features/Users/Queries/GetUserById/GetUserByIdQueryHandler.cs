using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler
    : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IApplicationDbContext _context;

        public GetUserByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(
    GetUserByIdQuery request,
    CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(u => u.UserId == request.UserId)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                throw new NotFoundException($"User with ID {request.UserId} not found");

            return user;
        }
    }
}

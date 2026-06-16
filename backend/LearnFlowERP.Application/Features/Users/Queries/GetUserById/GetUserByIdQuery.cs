using LearnFlowERP.Application.Features.Users.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(long UserId) : IRequest<UserDto>;
}

using MediatR;
using Microsoft.AspNetCore.Http;

namespace LearnFlowERP.Application.Features.Users.Commands.UpdateUserAvatar
{
    public record UpdateUserAvatarCommand(
    long UserId,
    string AvatarUrl
) : IRequest<Unit>;
}

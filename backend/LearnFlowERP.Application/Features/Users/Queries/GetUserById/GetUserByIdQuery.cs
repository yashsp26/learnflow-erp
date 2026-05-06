using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Application.Features.Users.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(long UserId) : IRequest<UserDto>;
}

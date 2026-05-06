using LearnFlowERP.Application.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LearnFlowERP.Application.Features.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Users.Queries.GetAllUsers;
using LearnFlowERP.Application.Features.Users.Commands.UpdateUserAvatar;
using LearnFlowERP.Application.Common.Models;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("check-auth")]
        public IActionResult SecureEndpoint()
        {
            return Ok(new { isAuthenticated = true, message = "You are authenticated" });
        }

        [Permission("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return Ok(userId);
        }

        [Permission("ViewUser")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }

        [Permission("ViewUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }


        [Permission("UpdateUser")]
        [HttpPatch("{id}/avatar")]
        public async Task<IActionResult> UpdateAvatar(long id, [FromBody] FileUrlDto dto)
        {
            await _mediator.Send(new UpdateUserAvatarCommand(id, dto.Url));
            return Ok(Url);
        }
    }
}
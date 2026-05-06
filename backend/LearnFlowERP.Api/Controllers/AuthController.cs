using LearnFlowERP.Application.Features.Auth.Commands.Login;
using LearnFlowERP.Application.Features.Auth.Commands.LogOut;
using LearnFlowERP.Application.Features.Auth.Commands.RefreshTokenCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableRateLimiting("LoginPolicy")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(new { token, message = "Login successful" });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh(RefreshTokenCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(new { token, message = "Token refreshed" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
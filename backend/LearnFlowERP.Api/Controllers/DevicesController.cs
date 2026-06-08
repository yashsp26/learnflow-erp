using LearnFlowERP.Application.Features.Devices.Commands.RegisterDevice;
using LearnFlowERP.Application.Features.Devices.Commands.UnregisterDevice;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/devices")]
    public class DevicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DevicesController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterDeviceCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete("unregister")]
        public async Task<IActionResult> Unregister(
            [FromBody] UnregisterDeviceCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
    }
}
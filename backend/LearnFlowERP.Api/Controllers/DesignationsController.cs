using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Designations.Commands.CreateDesignation;
using LearnFlowERP.Application.Features.Designations.Commands.DeleteDesignation;
using LearnFlowERP.Application.Features.Designations.Commands.UpdateDesignation;
using LearnFlowERP.Application.Features.Designations.Queries.GetDesignationById;
using LearnFlowERP.Application.Features.Designations.Queries.GetDesignations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/designations")]
    public class DesignationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DesignationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Permission("CreateDesignation")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateDesignationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Permission("ViewDesignation")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _mediator.Send(
                    new GetDesignationsQuery()));
        }

        [Permission("ViewDesignation")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            long id)
        {
            return Ok(
                await _mediator.Send(
                    new GetDesignationByIdQuery(id)));
        }

        [Permission("UpdateDesignation")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(
            long id,
            UpdateDesignationCommand command)
        {
            command.DesignationId = id;

            await _mediator.Send(command);

            return Ok();
        }

        [Permission("DeleteDesignation")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            long id)
        {
            await _mediator.Send(
                new DeleteDesignationCommand(id));

            return Ok();
        }
    }
}
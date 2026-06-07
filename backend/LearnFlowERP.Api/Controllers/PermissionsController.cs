using LearnFlowERP.Application.Features.Permissions.Commands.GrantDesignationPermission;
using LearnFlowERP.Application.Features.Permissions.Commands.GrantRolePermission;
using LearnFlowERP.Application.Features.Permissions.Commands.RevokeRolePermission;
using LearnFlowERP.Application.Features.Permissions.Queries.GetAllPermissions;
using LearnFlowERP.Application.Features.Permissions.Queries.GetRolePermissions;
using LearnFlowERP.Application.Features.Permissions.Commands.RevokeDesignationPermission;
using LearnFlowERP.Application.Features.Permissions.Queries.GetDesignationPermissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LearnFlowERP.Api.Authorization;
using LearnFlowERP.Application.Features.Payments.Queries.GetReceiptByPaymentId;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/permissions")]
    public class PermissionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _mediator.Send(
                    new GetAllPermissionsQuery()));
        }

        [HttpGet("roles/{roleId}")]
        public async Task<IActionResult> GetRolePermissions(
            long roleId)
        {
            return Ok(
                await _mediator.Send(
                    new GetRolePermissionsQuery(roleId)));
        }

        [HttpPost("roles/{roleId}/{permissionId}")]
        public async Task<IActionResult> GrantRolePermission(
            long roleId,
            long permissionId)
        {
            await _mediator.Send(
                new GrantRolePermissionCommand(
                    roleId,
                    permissionId));

            return Ok();
        }

        [HttpDelete("roles/{roleId}/{permissionId}")]
        public async Task<IActionResult> RevokeRolePermission(
            long roleId,
            long permissionId)
        {
            await _mediator.Send(
                new RevokeRolePermissionCommand(
                    roleId,
                    permissionId));

            return Ok();
        }

        [HttpGet("designations/{designationId}")]
        public async Task<IActionResult> GetDesignationPermissions(
    long designationId)
        {
            return Ok(
                await _mediator.Send(
                    new GetDesignationPermissionsQuery(
                        designationId)));
        }

        [HttpPost("designations/{designationId}/{permissionId}")]
        public async Task<IActionResult> GrantDesignationPermission(
            long designationId,
            long permissionId)
        {
            await _mediator.Send(
                new GrantDesignationPermissionCommand(
                    designationId,
                    permissionId));

            return Ok();
        }

        [HttpDelete("designations/{designationId}/{permissionId}")]
        public async Task<IActionResult> RevokeDesignationPermission(long designationId,long permissionId)
        {
            await _mediator.Send(
                new RevokeDesignationPermissionCommand(
                    designationId,
                    permissionId));

            return Ok();
        }

        [Permission("ViewPayment")]
        [HttpGet("{paymentId}/receipt")]
        public async Task<IActionResult> Receipt(long paymentId)
        {
            return Ok(
                await _mediator.Send(
                    new GetReceiptByPaymentIdQuery(paymentId)));
        }
    }
}

using MediatR;

namespace LearnFlowERP.Application.Features.Devices.Commands.UnregisterDevice
{
    public record UnregisterDeviceCommand(
        string DeviceToken)
        : IRequest<Unit>;
}
using LearnFlowERP.Domain.Enums;
using MediatR;

namespace LearnFlowERP.Application.Features.Devices.Commands.RegisterDevice
{
    public record RegisterDeviceCommand(
        string DeviceToken,
        DevicePlatform Platform)
        : IRequest<Unit>;
}
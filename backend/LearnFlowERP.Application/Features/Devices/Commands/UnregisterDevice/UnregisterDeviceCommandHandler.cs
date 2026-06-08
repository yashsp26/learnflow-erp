using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Devices.Commands.UnregisterDevice
{
    public class UnregisterDeviceCommandHandler
        : IRequestHandler<UnregisterDeviceCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public UnregisterDeviceCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            UnregisterDeviceCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId!.Value;

            var device = await _context.UserDevices
                .FirstOrDefaultAsync(
                    x => x.UserId == userId &&
                         x.DeviceToken == request.DeviceToken,
                    cancellationToken);

            if (device != null)
            {
                _context.UserDevices.Remove(device);

                await _context.SaveChangesAsync(
                    cancellationToken);
            }

            return Unit.Value;
        }
    }
}
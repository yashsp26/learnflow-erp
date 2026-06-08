using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Devices.Commands.RegisterDevice
{
    public class RegisterDeviceCommandHandler
        : IRequestHandler<RegisterDeviceCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public RegisterDeviceCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            RegisterDeviceCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId!.Value;

            var exists = await _context.UserDevices
                .AnyAsync(
                    x => x.UserId == userId &&
                         x.DeviceToken == request.DeviceToken,
                    cancellationToken);

            if (!exists)
            {
                _context.UserDevices.Add(
                    new UserDevice
                    {
                        UserId = userId,
                        DeviceToken = request.DeviceToken,
                        Platform = request.Platform,
                        IsActive = true
                    });

                await _context.SaveChangesAsync(
                    cancellationToken);
            }

            return Unit.Value;
        }
    }
}
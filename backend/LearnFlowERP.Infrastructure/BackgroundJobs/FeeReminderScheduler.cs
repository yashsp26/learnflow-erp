using LearnFlowERP.Application.Features.FeeReminders.Commands.SendAllFeeReminders;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LearnFlowERP.Infrastructure.BackgroundJobs
{
    public class FeeReminderScheduler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FeeReminderScheduler> _logger;

        public FeeReminderScheduler(
            IServiceProvider serviceProvider,
            ILogger<FeeReminderScheduler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Fee Reminder Scheduler Started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.Now;

                    var nextRun = DateTime.Today
                        .AddHours(18)
                        .AddMinutes(13);

                    var delay = TimeSpan.FromMinutes(1);

                    _logger.LogInformation(
                        "Next fee reminder run at {NextRun}",
                        nextRun);

                    await Task.Delay(
                        delay,
                        stoppingToken);

                    using var scope =
                        _serviceProvider.CreateScope();

                    var mediator =
                        scope.ServiceProvider
                            .GetRequiredService<IMediator>();

                    var count =
                        await mediator.Send(
                            new SendAllFeeRemindersCommand(),
                            stoppingToken);

                    _logger.LogInformation(
                        "{Count} fee reminders sent",
                        count);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error executing fee reminder scheduler");

                    await Task.Delay(
                        TimeSpan.FromMinutes(5),
                        stoppingToken);
                }
            }
        }
    }
}
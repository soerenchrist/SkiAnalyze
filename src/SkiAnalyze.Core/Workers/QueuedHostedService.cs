using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SkiAnalyze.SharedKernel.Interfaces;

namespace SkiAnalyze.Core.Workers;

public class QueuedHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly ILogger<QueuedHostedService> _logger;

    public QueuedHostedService(
        IServiceProvider serviceProvider,
        IBackgroundTaskQueue taskQueue,
        ILogger<QueuedHostedService> logger)
        => (_serviceProvider, _taskQueue, _logger) = (serviceProvider, taskQueue, logger);

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(QueuedHostedService)} is running.");

        return ProcessTaskQueueAsync(stoppingToken);
    }

    private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                Func<IServiceProvider, CancellationToken, ValueTask>? workItem =
                    await _taskQueue.DequeueAsync(stoppingToken);

                await workItem(_serviceProvider, stoppingToken);
            } catch(OperationCanceledException)
            {

            } catch(Exception ex)
            {
                _logger.LogError(ex, "Error occured executing work item");
            }
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            $"{nameof(QueuedHostedService)} is stopping.");

        await base.StopAsync(stoppingToken);
    }
}

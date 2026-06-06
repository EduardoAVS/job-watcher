using JobWatcher.Application.Scraping;
using JobWatcher.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JobWatcher.Worker;

public class MonitoringWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<MonitoringWorker> _logger;

    public MonitoringWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<MonitoringWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Monitoring worker started.");

        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ExecuteMonitoringCycleAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                // A aplicação está encerrando. Não é erro real.
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception,
                    "An error occurred while executing the monitoring cycle.");
            }

            await timer.WaitForNextTickAsync(stoppingToken);
        }
    }

    private async Task ExecuteMonitoringCycleAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker tick: {Time}", DateTimeOffset.UtcNow);

        using var scope = _scopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        var scraperFactory = scope.ServiceProvider
            .GetRequiredService<IJobSourceScraperFactory>();

        var jobSources = await dbContext.JobSources
            .Include(jobSource => jobSource.Company)
            .ToListAsync(cancellationToken);

        _logger.LogInformation(
            "Loaded {Count} job sources.",
            jobSources.Count);

        foreach (var jobSource in jobSources)
        {
            var scraper = scraperFactory.Get(jobSource.SourceType);

            var scrapedJobs = await scraper.ScrapeAsync(
                jobSource,
                cancellationToken);

            _logger.LogInformation(
                "JobSource {JobSourceId} from company {CompanyName} using {SourceType} returned {Count} jobs.",
                jobSource.Id,
                jobSource.Company.Name,
                jobSource.SourceType,
                scrapedJobs.Count);
        }
    }
}
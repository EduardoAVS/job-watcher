using JobWatcher.Application.Scraping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JobWatcher.Worker;

public sealed class ScraperDiTestWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ScraperDiTestWorker> _logger;

    public ScraperDiTestWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<ScraperDiTestWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();

        var scraperFactory = scope.ServiceProvider
            .GetRequiredService<IJobSourceScraperFactory>();

        var scraper = scraperFactory.Get(JobSourceType.Greenhouse);

        _logger.LogInformation(
            "Scraper resolved successfully: {ScraperName}",
            scraper.GetType().Name);

        return Task.CompletedTask;
    }
}
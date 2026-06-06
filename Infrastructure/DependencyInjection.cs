using JobWatcher.Application.Scraping;
using JobWatcher.Infrastructure.Scraping;
using JobWatcher.Infrastructure.Scraping.Greenhouse;
using Microsoft.Extensions.DependencyInjection;

namespace JobWatcher.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient<IJobSourceScraper, GreenhouseJobSourceScraper>();

        services.AddScoped<IJobSourceScraperFactory, JobSourceScraperFactory>();

        return services;
    }
}
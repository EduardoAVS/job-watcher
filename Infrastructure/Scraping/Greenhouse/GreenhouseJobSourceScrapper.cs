using JobWatcher.Application.Scraping;
using JobWatcher.Domain.Entities;
using System.Net.Http.Json;

namespace JobWatcher.Infrastructure.Scraping.Greenhouse;

public sealed class GreenhouseJobSourceScraper : IJobSourceScraper
{
    private readonly HttpClient _httpClient;

    public JobSourceType SourceType => JobSourceType.Greenhouse;

    public GreenhouseJobSourceScraper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<ScrapedJobPosting>> ScrapeAsync(
        JobSource source,
        CancellationToken cancellationToken)
    {
        using var response = await _httpClient.GetAsync(source.Url, cancellationToken);

        response.EnsureSuccessStatusCode();

        // Por enquanto pode deixar assim e implementar o parse no próximo passo.
        return [];
    }
}
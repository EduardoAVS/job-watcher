using System.Net.Http.Json;
using JobWatcher.Application.Scraping;
using JobWatcher.Domain.Entities;

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
        var response = await _httpClient.GetFromJsonAsync<GreenhouseJobBoardResponse>(
            source.Url,
            cancellationToken);

        if (response is null)
        {
            return [];
        }

        return response.Jobs
            .Where(job =>
                !string.IsNullOrWhiteSpace(job.Title) &&
                !string.IsNullOrWhiteSpace(job.AbsoluteUrl))
            .Select(job => new ScrapedJobPosting
            {
                Title = job.Title!,
                Url = job.AbsoluteUrl!,
                Location = job.Location?.Name,
                Description = job.Content,
                Department = GetDepartment(job),
                PublishedAt = job.FirstPublished,
                UpdatedAt = job.UpdatedAt
            })
            .ToList();
    }
}
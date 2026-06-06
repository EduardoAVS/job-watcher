using JobWatcher.Domain.Entities;

namespace JobWatcher.Application.Scraping;

public interface IJobSourceScraper
{
    JobSourceType SourceType { get; }

    Task<IReadOnlyList<ScrapedJobPosting>> ScrapeAsync(
        JobSource source,
        CancellationToken cancellationToken);
}
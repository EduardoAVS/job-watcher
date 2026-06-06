using JobWatcher.Application.Scraping;

namespace JobWatcher.Infrastructure.Scraping;

public sealed class JobSourceScraperFactory : IJobSourceScraperFactory
{
    private readonly IEnumerable<IJobSourceScraper> _scrapers;

    public JobSourceScraperFactory(IEnumerable<IJobSourceScraper> scrapers)
    {
        _scrapers = scrapers;
    }

    public IJobSourceScraper Get(JobSourceType sourceType)
    {
        return _scrapers.FirstOrDefault(scraper => scraper.SourceType == sourceType)
            ?? throw new InvalidOperationException(
                $"No scraper registered for source type '{sourceType}'.");
    }
}
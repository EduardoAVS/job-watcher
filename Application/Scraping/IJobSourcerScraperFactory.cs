namespace JobWatcher.Application.Scraping;

public interface IJobSourceScraperFactory
{
    IJobSourceScraper Get(JobSourceType sourceType);
}
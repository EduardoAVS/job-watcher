namespace JobWatcher.Application.Scraping;

public sealed class ScrapedJobPosting
{
    public string Title { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public string? Location { get; init; }
    public string? Description { get; init; }
    public DateTimeOffset? PublishedAt { get; init; }
}
namespace JobWatcher.Domain.Entities;

public class JobPosting
{
    public long Id { get; private set; }
    public string Url { get; private set; } // Unique
    public long TrackedPageId { get; private set; }
    public TrackedPage TrackedPage { get; private set; }
    public DateTimeOffset FirstSeenAt { get; private set; }
    public JobPosting(string url, TrackedPage trackedPage)
    {
        Url = url;
        TrackedPage = trackedPage;
        TrackedPageId = trackedPage.Id;
        FirstSeenAt = DateTimeOffset.UtcNow;
    }
    private JobPosting() { }
}
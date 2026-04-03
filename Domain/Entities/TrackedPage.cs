namespace JobWatcher.Domain.Entities;

public class TrackedPage
{
    public long Id { get; private set; }
    public string Url { get; private set; } // Unique no SGBD
    public DateTimeOffset CreatedAt { get; private set; }
    public TrackedPage(string url)
    {
        Url = url;
        CreatedAt = DateTimeOffset.UtcNow;
    }
    private TrackedPage() { }
}
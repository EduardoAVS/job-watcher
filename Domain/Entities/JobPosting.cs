namespace JobWatcher.Domain.Entities;

public class JobPosting
{
    public long Id { get; private set; }

    public long CompanyId { get; private set; }
    public Company Company { get; private set; } = null!;

    public long JobSourceId { get; private set; }
    public JobSource JobSource { get; private set; } = null!;

    public string Title { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty; // Unique

    public SeniorityLevel? Level { get; private set; }
    public string? Location { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? LastSeenAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public JobPosting(
        long companyId,
        long jobSourceId,
        string title,
        string url,
        SeniorityLevel? level = null,
        string? location = null)
    {
        CompanyId = companyId;
        JobSourceId = jobSourceId;
        Title = title;
        Url = url;
        Level = level;
        Location = location;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private JobPosting()
    {
    }
}
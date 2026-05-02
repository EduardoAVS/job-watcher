namespace JobWatcher.Domain.Entities;

public class JobSource
{
    public long Id { get; private set; }
    public long CompanyId { get; private set; }
    public Company Company { get; private set; } = null!;

    public string Url { get; private set; } = string.Empty;
    public JobSourceType SourceType { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? LastCheckedAt { get; private set; }

    public JobSource(long companyId, string url, JobSourceType sourceType)
    {
        CompanyId = companyId;
        Url = url;
        SourceType = sourceType;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private JobSource()
    {
    }
}
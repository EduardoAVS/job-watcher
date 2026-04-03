namespace JobWatcher.Domain.Entities;

public class Notification
{
    public long UserId { get; private set; }
    public long JobPostingId { get; private set; }

    public NotificationStatus Status { get; private set; }

    public int RetryCount { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? SentAt { get; private set; }

    public Notification(long userId, long jobPostingId)
    {
        UserId = userId;
        JobPostingId = jobPostingId;
        Status = NotificationStatus.Pending;
        RetryCount = 0;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private Notification() { }
}
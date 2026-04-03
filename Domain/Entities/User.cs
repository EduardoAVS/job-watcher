namespace JobWatcher.Domain.Entities;

public class User
{
    public long Id { get; private set; }
    public long TelegramId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public User(long telegramId)
    {
        TelegramId = telegramId;
        CreatedAt = DateTimeOffset.UtcNow;
    }
    private User() { }
}
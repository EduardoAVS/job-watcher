namespace JobWatcher.Domain.Entities;

public class UserPreferences
{
    public long UserId { get; private set; }
    public string? Stack { get; private set; }
    public string? Location { get; private set; }
    public WorkModel? WorkModel { get; private set; }
    public SeniorityLevel? Level { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public UserPreferences(
        long userId,
        string? stack = null,
        string? location = null,
        WorkModel? workModel = null,
        SeniorityLevel? level = null
    )
    {
        UserId = userId;
        Stack = stack;
        Location = location;
        WorkModel = workModel;
        Level = level;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
    private UserPreferences() { }
}
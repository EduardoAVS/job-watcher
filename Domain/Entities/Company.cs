namespace JobWatcher.Domain.Entities;

public class Company
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public Company(string name)
    {
        Name = name;
        CreatedAt = DateTimeOffset.UtcNow;
    }
    private Company() { }
}
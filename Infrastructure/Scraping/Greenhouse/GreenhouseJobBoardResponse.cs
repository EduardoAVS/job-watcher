using System.Text.Json.Serialization;

namespace JobWatcher.Infrastructure.Scraping.Greenhouse;

internal sealed class GreenhouseJobBoardResponse
{
    [JsonPropertyName("jobs")]
    public List<GreenhouseJob> Jobs { get; init; } = [];
}

internal sealed class GreenhouseJob
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("absolute_url")]
    public string? AbsoluteUrl { get; init; }

    [JsonPropertyName("location")]
    public GreenhouseLocation? Location { get; init; }

    [JsonPropertyName("content")]
    public string? Content { get; init; }
    [JsonPropertyName("departments")]
    public List<GreenhouseDepartment> Departments { get; init; } = [];

    [JsonPropertyName("first_published")]
    public DateTimeOffset? FirstPublished { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; init; }
}

internal sealed class GreenhouseLocation
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

internal sealed class GreenhouseDepartment
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
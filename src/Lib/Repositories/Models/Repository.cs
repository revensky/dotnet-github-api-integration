using System.Text.Json.Serialization;

namespace Revensky.GitHubApiIntegration.Lib.Repositories.Models;

public class Repository
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("html_url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("fork")]
    public bool Fork { get; set; }
}

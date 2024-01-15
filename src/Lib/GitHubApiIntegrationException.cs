using System.Text.Json.Serialization;

namespace Revensky.GitHubApiIntegration.Lib;

public class GitHubApiIntegrationException : Exception
{
    public const string REPOSITORY_NOT_FOUND = "REPOSITORY_NOT_FOUND";

    [JsonPropertyName("code")]
    public string Code { get; private set; }

    [JsonPropertyName("description")]
    public string Description { get; private set; }

    public GitHubApiIntegrationException(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public static GitHubApiIntegrationException RepositoryNotFound(string repository)
    {
        return new GitHubApiIntegrationException(REPOSITORY_NOT_FOUND, $"Could not find the repository \"{repository}\".");
    }
}

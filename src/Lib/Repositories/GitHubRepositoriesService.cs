using System.Net;
using System.Net.Http.Json;
using Revensky.GitHubApiIntegration.Lib.Repositories.Models;

namespace Revensky.GitHubApiIntegration.Lib.Repositories;

public class GitHubRepositoriesService : IGitHubRepositoriesService
{
    private readonly HttpClient _httpClient;

    public GitHubRepositoriesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Repository[]> GetUserRepositoriesAsync(string username)
    {
        var response = await _httpClient.GetAsync($"https://api.github.com/users/{username}/repos");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to get user repos.");
        }

        return (await response.Content.ReadFromJsonAsync<Repository[]>())!;
    }

    public async Task<Repository?> GetUserRepositoryAsync(string username, string repository)
    {
        var response = await _httpClient.GetAsync($"https://api.github.com/repos/{username}/{repository}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw GitHubApiIntegrationException.RepositoryNotFound(repository);
        }

        return await response.Content.ReadFromJsonAsync<Repository>();
    }
}

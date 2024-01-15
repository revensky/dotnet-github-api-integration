using Revensky.GitHubApiIntegration.Lib.Repositories.Models;

namespace Revensky.GitHubApiIntegration.Lib.Repositories;

public interface IGitHubRepositoriesService
{
    Task<Repository[]> GetUserRepositoriesAsync(string username);

    Task<Repository?> GetUserRepositoryAsync(string username, string repository);
}

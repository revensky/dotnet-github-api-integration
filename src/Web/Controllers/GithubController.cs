using Microsoft.AspNetCore.Mvc;
using Revensky.GitHubApiIntegration.Lib;
using Revensky.GitHubApiIntegration.Lib.Repositories;
using Revensky.GitHubApiIntegration.Web.Models;

namespace Revensky.GitHubApiIntegration.Web.Controllers;

public class GithubController : Controller
{
    private readonly IGitHubRepositoriesService _repositoriesService;

    public GithubController(IGitHubRepositoriesService repositoriesService)
    {
        _repositoriesService = repositoriesService;
    }

    public async Task<ActionResult> Repositories([FromQuery] string username)
    {
        var repositories = await _repositoriesService.GetUserRepositoriesAsync(username);
        return View(repositories);
    }

    public async Task<ActionResult> Repository([FromQuery] string username, [FromQuery(Name = "repository")] string repositoryName)
    {
        try
        {
            var repository = await _repositoriesService.GetUserRepositoryAsync(username, repositoryName);
            return View(repository);
        }
        catch (GitHubApiIntegrationException)
        {
            return View("Error", new ErrorViewModel
            {
                Message = $"Could not find the repository \"{repositoryName}\" for the user \"{username}\"."
            });
        }
    }
}

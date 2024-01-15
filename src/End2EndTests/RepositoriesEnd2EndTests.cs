using NUnit.Framework;
using Revensky.GitHubApiIntegration.Lib;
using Revensky.GitHubApiIntegration.Lib.Repositories;
using Revensky.GitHubApiIntegration.Lib.Repositories.Models;

namespace Revensky.GitHubApiIntegration.End2EndTests;

[TestFixture]
public class RepositoriesEnd2EndTests
{
    private HttpClient _httpClient;

    private IGitHubRepositoriesService _service;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();

        _httpClient.DefaultRequestHeaders.Add(
            "User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.38 Safari/537.36 Brave/75");

        _service = new GitHubRepositoriesService(_httpClient);
    }

    [Test]
    public async Task GetUserRepositoriesAsync_ShouldReturnAnArrayOfRepositories()
    {
        var username = "octocat";

        var repositories = await _service.GetUserRepositoriesAsync(username);

        Assert.Multiple(() =>
        {
            Assert.That(repositories, Is.Not.Null);
            Assert.That(repositories, Is.InstanceOf<Repository[]>());
        });
    }

    [Test]
    public void GetUserRepository_ShouldThrowGitHubApiIntegrationException_WhenTheRepositoryDoesNotExist()
    {
        var username = "octocat";
        var repositoryName = "bogus-repository-name";

        Assert.ThrowsAsync<GitHubApiIntegrationException>(async () => await _service.GetUserRepositoryAsync(username, repositoryName));
    }

    [Test]
    public async Task GetUserRepositoryAsync_ShouldReturnAValidRepository_WhenItExists()
    {
        var username = "octocat";
        var repositoryName = "Hello-World";

        var repository = await _service.GetUserRepositoryAsync(username, repositoryName);

        Assert.Multiple(() =>
        {
            Assert.That(repository, Is.Not.Null);
            Assert.That(repository, Is.InstanceOf<Repository>());
            Assert.That(repository!.Name, Is.EqualTo("Hello-World"));
            Assert.That(repository!.Url, Is.EqualTo("https://github.com/octocat/Hello-World"));
            Assert.That(repository!.Description, Is.EqualTo("My first repository on GitHub!"));
            Assert.That(repository!.Fork, Is.False);
        });
    }
}

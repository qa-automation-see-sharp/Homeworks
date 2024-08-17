using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;

    [SetUp]
    public void Setup()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }

    [Test]
    public async Task CreateUser201()
    {
        var user = new User
        {
            FullName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            NickName = Guid.NewGuid().ToString()
        };

        var httpResponseMessage = await _libraryHttpService.CreateUser(user);
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<User>(content);

        Assert.Multiple(() =>
        {
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.FullName, Is.EqualTo(user.FullName));
            Assert.That(response.NickName, Is.EqualTo(user.NickName));
        });
    }

    [Test]
    public async Task CreateUser400()
    {
        var user = new User
        {
            FullName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            NickName = Guid.NewGuid().ToString()
        };

        await _libraryHttpService.CreateUser(user);
        var httpResponseMessage = await _libraryHttpService.CreateUser(user);

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Login200()
    {
        var user = new User
        {
            FullName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            NickName = Guid.NewGuid().ToString()
        };

        await _libraryHttpService.CreateUser(user);
        var httpResponseMessage = await _libraryHttpService.LogIn(user);
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<AuthorizationToken>(content);

        Assert.Multiple(() =>
        {
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Token, Is.Not.Null);
            Assert.That(response.NickName, Is.EqualTo(user.NickName));
        });
    }

    [Test]
    public async Task Login400()
    {
        var user = new User
        {
            FullName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            NickName = Guid.NewGuid().ToString()
        };

        var httpResponseMessage = await _libraryHttpService.LogIn(user);

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }    
}
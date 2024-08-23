using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task RegisterUserAsync()
    {
        var user = new User
        {
            FullName = Guid.NewGuid().ToString(),
            NickName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
        };
        
        var response = await LibraryHttpService.CreateUser(user);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }

    [Test]
    public async Task RegisterExistingAsync()
    {
        var response = await LibraryHttpService.CreateUser(Users.First().Key);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task LogInAsync()
    {
        var response = await LibraryHttpService.LogIn(Users.First().Key);
        var token = await response.Content.ReadAsStringAsync();
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(token, !Is.Empty);
        });
    }
}
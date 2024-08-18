using Bogus;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService = new();

    [OneTimeSetUp]
    public new async Task OneTimeSetup()
    {
        var client = _libraryHttpService.Configure("http://localhost:5111/");
    }
    private User GenerateUser()
    {
        var faker = new Faker();

        return new User()
        {
            FullName = "David Solis",
            NickName = $"soledavi{faker.Random.AlphaNumeric(4)}",
            Password = "126rtgc"
        };
    }

    //TODO cover with tests all endpoints from Users controller
    // Create user
    // Log In

    [Test]
    public async Task CreateUser()
    {
        var user = GenerateUser();
        HttpResponseMessage response = await _libraryHttpService.CreateUser(user);

        var jsonString = await response.Content.ReadAsStringAsync();

        var users = JsonConvert.DeserializeObject<User>(jsonString);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.That(users.FullName, Is.EqualTo(user.FullName));
            Assert.That(users.NickName, Is.EqualTo(user.NickName));
        });
    }

    [Test]
    public async Task LogIn()
    {
        var user = GenerateUser();

        await _libraryHttpService.CreateUser(user);

        HttpResponseMessage response = await _libraryHttpService.LogIn(user);

        var jsonString = await response.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(jsonString, Is.Not.Null);
        });
    }
}
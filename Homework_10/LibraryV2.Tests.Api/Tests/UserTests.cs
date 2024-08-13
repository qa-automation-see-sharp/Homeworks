using Bogus;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;
public class UsersTests : LibraryV2TestFixture
{
    [SetUp]
    public void Setup()
    {
        LibraryHttpService = new LibraryHttpService();
        LibraryHttpService.Configure("http://localhost:5111/");
    }

    //TODO cover with tests all endpoints from Users controller
    // Create user
    // Log In

    [Test]
    public async Task CreateUser()
    {
        var faker = new Faker();

        var user = new User()
        {
            FullName = "David Solis",
            NickName = $"soledavi{faker.Random.AlphaNumeric(4)}",
            Password = "126rtgc"
        };

        HttpResponseMessage response = await LibraryHttpService.CreateUser(user);

        var jsonString = await response.Content.ReadAsStringAsync();

        var users = JsonConvert.DeserializeObject<User>(jsonString);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.That(users.FullName, Is.EqualTo(user.FullName));
            Assert.That(users.NickName, Is.EqualTo(user.NickName));
            Assert.That(users.Password, Is.EqualTo(user.Password));
        });
    }

    [Test]
    public async Task LogIn()
    {
        HttpResponseMessage response = await LibraryHttpService.LogIn(User);

        var jsonString = await response.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(jsonString, Is.Not.Null);
        });
    }
}
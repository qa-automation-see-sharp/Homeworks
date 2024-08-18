using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    //TODO cover with tests all endpoints from Users controller
    // Create user
    // Log In
    private User User { get; set; }

    [OneTimeSetUp]
    public async Task SetUp()
    {
        var client = _httpService.Configure("http://localhost:5111/");
        User = await client.CreateDefaultUser();
    }

    [Test]
    public async Task CreateUserSusses()
    {
        User user = new()
        {
            FullName = "Robert Finch" + Guid.NewGuid(),
            Password = "Qwerty",
            NickName = "Finch" + Guid.NewGuid()
        };

        var response = await _httpService.CreateUser(user);
        var json = await response.Content.ReadAsStringAsync();
        var u = JsonConvert.DeserializeObject<User>(json);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.That(response, Is.Not.Null);
            Assert.That(u.FullName, Is.EqualTo(user.FullName));
            Assert.That(u.NickName, Is.EqualTo(user.NickName));
        });
    }

    [Test]
    public async Task LoginUser()
    {
        var message = await _httpService.LogIn(User);
        var json = await message.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<AuthorizationToken>(json);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.Token, Is.Not.Empty);
            Assert.That(obj.NickName, Is.EqualTo(User.NickName));
        });
    }
}
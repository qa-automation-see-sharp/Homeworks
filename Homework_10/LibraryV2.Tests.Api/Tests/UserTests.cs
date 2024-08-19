using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    private User User { get; set; }

    [OneTimeSetUp]
    public async Task SetUp()
    {
        User = await HttpService.CreateDefaultUser();
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

        var response = await HttpService.CreateUser(user);
        var json = await response.Content.ReadAsStringAsync();
        var u = JsonConvert.DeserializeObject<User>(json);

        Assert.Multiple(() =>
        {
            // Зараз рекомендують використовувати Assert.That, бо він дає більше інформації про помилку
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response, Is.Not.Null);
            Assert.That(u.FullName, Is.EqualTo(user.FullName));
            Assert.That(u.NickName, Is.EqualTo(user.NickName));
        });
    }

    [Test]
    public async Task LoginUser()
    {
        var message = await HttpService.LogIn(User);
        var json = await message.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<AuthorizationToken>(json);

        Assert.Multiple(() =>
        {
            // Зараз рекомендують використовувати Assert.That, бо він дає більше інформації про помилку
            Assert.That(message.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.Token, Is.Not.Empty);
            Assert.That(obj.NickName, Is.EqualTo(User.NickName));
        });
    }
}
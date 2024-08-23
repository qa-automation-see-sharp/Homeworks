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
    public void OneTimeSetup()
    {
        var client = _libraryHttpService.Configure("http://localhost:5111/");
    }

    private User GenerateUser()
    {
        var faker = new Faker();

        return new User
        {
            FullName = "David Solis",
            NickName = $"soledavi{faker.Random.AlphaNumeric(4)}",
            Password = "126rtgc"
        };
    }

    [Test]
    public async Task CreateUser_WhenDataIsValid_ReturnCreated()
    {
        var user = GenerateUser();
        HttpResponseMessage response = await _libraryHttpService.CreateUser(user);

        var jsonString = await response.Content.ReadAsStringAsync();

        var users = JsonConvert.DeserializeObject<User>(jsonString);

        Assert.Multiple(() =>
        {
            // Зараз рекомендується використовувати Assert.That замість Assert.AreEqual
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(users.FullName, Is.EqualTo(user.FullName));
            Assert.That(users.NickName, Is.EqualTo(user.NickName));
        });
    }

    [Test]
    public async Task LogIn_WhenUserExists_ReturnOk()
    {
        var user = GenerateUser();

        await _libraryHttpService.CreateUser(user);

        HttpResponseMessage response = await _libraryHttpService.LogIn(user);

        var jsonString = await response.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            // Зараз рекомендується використовувати Assert.That замість Assert.AreEqual
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonString, Is.Not.Null);
        });
    }
}
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;
    private User user;
    private string _token;

    [SetUp]
    public void Setup()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }
    [Test]

    public async Task CreateUsers() 
    {
        var user = new User()
        {
            FullName = "Test User",
            NickName = "Test Nickname",
            Password = "password"
        };

        HttpResponseMessage response = await _libraryHttpService.CreateUser(user);

        var jsonString = await response.Content.ReadAsStringAsync();

        var userResponse = JsonConvert.DeserializeObject<User>(jsonString);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(userResponse.FullName, Is.EqualTo(user.FullName));
            
        });
    }

    //TODO cover with tests all endpoints from Users controller
    // Create user
    // Log In
}
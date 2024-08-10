using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class UsersTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;
    public string Token { get; private set; }

    [SetUp]
    public void Setup()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }

    //TODO cover with tests all endpoints from Users controller
    // Create user
    // Log In

    [Test, Order(1)]
    public async Task CreateUser()
    {
        var user = new User()
        {
            FullName = "Percy Smith",
            NickName = "locksmith",
            Password = "cg2ir37"
        };

        User? response = await _libraryHttpService.CreateUser(user);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.FullName, Is.EqualTo(user.FullName));
            Assert.That(response.NickName, Is.EqualTo(user.NickName));
            Assert.That(response.Password, Is.EqualTo(user.Password));
        });
    }

    [Test, Order(2)]
    public async Task LogIn()
    {
        var user = new User();
        user.NickName = "locksmith";
        user.Password = "cg2ir37";

        AuthorizationToken? response = await _libraryHttpService.LogIn(user);
        Token = response.Token;

        Assert.Multiple(() =>
        {
            Assert.That(response.Token, Is.Not.Null);
        });
    }
}
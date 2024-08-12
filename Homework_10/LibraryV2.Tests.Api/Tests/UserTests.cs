using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;

namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    //TODO cover with tests all endpoints from Users controller
    // Create user
    // Log In

    [Test]
    public async Task CreateUserSusses()
    {
        User user = new()
        {
            FullName = "Robert Finch" + Guid.NewGuid(),
            Password = "Qwerty",
            NickName = "Finch" + Guid.NewGuid()
        };

        User? response = await _libraryService.CreateUser(user);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.FullName, Is.EqualTo(user.FullName));
            Assert.That(response.NickName, Is.EqualTo(user.NickName));
            Assert.That(response.Password, Is.EqualTo(user.Password));
        });
    }

    [Test]
    public async Task LoginUser()
    {

        var user = _users.First();
        var response = await _libraryService.LogIn(user.Key);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Token, Is.Not.Empty);
            Assert.That(response.Token.Contains(user.Value));
        });
    }
}
using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.TestHelper;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    [Test]
    public async Task CreateUser_ShouldReturnOk()
    {
        // Arrange
        var user = DataHelper.CreateUser();

        // Act
        var response = await LibraryHttpService.CreateUser(user);
        var jsonString = await response.Content.ReadAsStringAsync();
        var createdUser = JsonConvert.DeserializeObject<User>(jsonString);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(createdUser.NickName, Is.EqualTo(user.NickName));
            Assert.That(createdUser.FullName, Is.EqualTo(user.FullName));
            Assert.That(createdUser.Password, Is.EqualTo(null));
        });
    }

    [Test]
    public async Task CreateUser_ThatAlreadyExists_ShouldReturnBadRequest()
    {
        // Arrange
        var user = DataHelper.CreateUser();
        var response1 = await LibraryHttpService.CreateUser(user);

        // Act
        var response2 = await LibraryHttpService.CreateUser(user);
        var jsonString = await response2.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response2.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(jsonString, Is.EqualTo($"\"User with nickname {user.NickName} already exists\""));
        });
    }
}
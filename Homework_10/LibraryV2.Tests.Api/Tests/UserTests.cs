using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Tests.Api.TestHelpers;

namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    [SetUp]
    public void Setup()
    {
    }

    [Test, Order(1)]
    [Description("This test checks the registration process of the user")]
    public async Task RegisterUserAsync_ReturnCreated()
    {
        var response = await _libraryHttpService.CreateUser(DataHelper.CreateUser());
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }

    [Test, Order(2)]
    [Description("This test checks if the user already exists in the system")]
    public async Task RegisterExistingAsync_ReturnBadRequest()
    {
        var response = await _libraryHttpService.CreateUser(_libraryHttpService.TestUsers.First().Key);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test, Order(3)]
    [Description("This test checks the login process of the user")]

    public async Task LogInAsync_ReturnOK()
    {
        var response = await _libraryHttpService.LogIn(_libraryHttpService.DefaultUser, false);
        var token = await response.Content.ReadAsStringAsync();
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(token, !Is.Empty);
        });
    }
}
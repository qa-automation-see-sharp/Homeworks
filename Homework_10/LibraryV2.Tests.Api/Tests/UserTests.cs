using LibraryV2.Models;
using LibraryV2.Services;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;

    [SetUp]
    public void Setup()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");


    }

    //TODO cover with tests all endpoints from Users controller
    // Create user
    // Log In

    [Test]
    public async Task CreateUser()
    {
        var usertocreate = GenerateTestUser();
        HttpResponseMessage responce = await _libraryHttpService.CreateUser(usertocreate);
        var jsonString = await responce.Content.ReadAsStringAsync();
        var userToAssert = JsonConvert.DeserializeObject<User>(jsonString);

        Assert.Multiple(() =>
        {
            Assert.That(responce.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(userToAssert.FullName, Is.EqualTo(usertocreate.FullName));
            Assert.That(userToAssert.NickName, Is.EqualTo(usertocreate.NickName));
        }
        );
    }
    [Test]
    public async Task LogInUser()
    {
        var usertocreate = GenerateTestUser();
        HttpResponseMessage responceCreateUser = await _libraryHttpService.CreateUser(usertocreate);
        HttpResponseMessage responceLoginUser = await _libraryHttpService.LogIn(usertocreate);
        var jsonString = await responceLoginUser.Content.ReadAsStringAsync();
        var userToAssert = JsonConvert.DeserializeObject<User>(jsonString);
        
        Assert.Multiple(() =>
        {
            Assert.That(responceLoginUser.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(userToAssert.NickName, Is.EqualTo(usertocreate.NickName));
            
        }
       );
    }
    private User GenerateTestUser()
    {
        return new User
        {
            FullName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            NickName = Guid.NewGuid().ToString()
        };
    }

}
using System.Net;
using System.Net.Http.Json;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using NuGet.Frameworks;

namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    //create variables
    private LibraryHttpService _libraryHttpService;
    private User _testUser;
    private readonly string _time = DateTime.Now.ToString("yyyyMMddHHmmss");
    private string _token;

    [SetUp]
    public async Task Setup()
    {
        //configure client
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
        //create new instance of User
        var _testUser = new User
        {
            FullName = "Papadzilla J. Martinson",
            NickName = "Some1",
            Password = "Password"
        };
        //send request to create new user
        var testUserResponse = await _libraryHttpService.CreateUser(_testUser);
        //get response
        var responseBodeTestUser = await testUserResponse.Content.ReadAsStringAsync();
        //send request to login
        var loginResponse = await _libraryHttpService.LogIn(_testUser);
        //get token
        _token = (await loginResponse.Content.ReadAsStringAsync()).Trim('"');
    }

    [Test]
    public async Task CreateNewUser_ReturnsOK()
    {
        //create new instance of User
        var user = new User
        {
            FullName = "Papadzilla",
            NickName = "Some2" + _time, //unique nickname for each test
            Password = "Password"
        };

        //send request to create new user
        var response = await _libraryHttpService.CreateUser(user);
        //get response
        var responseBody = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(responseBody);
        });
        
    }

    [Test]
    public async Task CreateUserWithDuplicateName_returnsBadRequest()
    {
        //create new user with duplicate username:
        var existedUser = new User
        {
            FullName = "NewName",
            NickName = "Some1", //existing nickname
            Password = "Password"
        };

        //send request to create new user
        var duplicateResponse = await _libraryHttpService.CreateUser(existedUser);
        var responseBody = await duplicateResponse.Content.ReadAsStringAsync();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, duplicateResponse.StatusCode);
            Assert.NotNull(responseBody);
        });
    }

    [Test]
    public async Task CreateUserWithBlankName_returnsBadRequest()
    {
        //create new user with blank username:
        var blankUser = new User
        {
            FullName = "NewName",
            NickName = "", //empty nickname
            Password = "Password"
        };

        //send request to create new user
        var blankResponse = await _libraryHttpService.CreateUser(blankUser);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, blankResponse.StatusCode);
            Assert.NotNull(blankResponse);
        });
    }

    [Test]
    public async Task CreateUserWithSpacedName_returnsBadRequest()
    {
        //create new user with spaced username:
        var spacedUser = new User
        {
            FullName = "NewName",
            NickName = "   ", //3 (three) spaces
            Password = "Password"
        };

        //send request to create new user
        var spacedResponse = await _libraryHttpService.CreateUser(spacedUser);
        var responseBody = await spacedResponse.Content.ReadAsStringAsync();

        //Assert
        Assert.Multiple (( )=> 
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, spacedResponse.StatusCode);
            Assert.NotNull(responseBody);
        });
    }

    [Test]
    public async Task CreateUserWithBlankPassword_returnsBadRequest()
    {
        //create new user with blank password:
        var blankPassword = new User
        {
            FullName = "NewName",
            NickName = "test" + _time, //unique nickname for each test
            Password = "" //empty password
        };

        //send request to create new user with blank password
        var blankResponse = await _libraryHttpService.CreateUser(blankPassword);
        var responseBody = await blankResponse.Content.ReadAsStringAsync();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, blankResponse.StatusCode);
            Assert.NotNull(responseBody);
        });
    }

    //===> CREATE USER WITH BLANK FULLNAME
    [Test]
    public async Task CreateUserWithBlankFullName_returnsCreated()
    {
        //create new user with blank fullName:
        var blankFullName = new User
        {
            FullName = "", //empty fullName
            NickName = "test" + _time, //unique nickname for each test
            Password = "Password"
        };

        //send request to create new user with blank fullName
        var blankResponse = await _libraryHttpService.CreateUser(blankFullName);
        var responseBody = await blankResponse.Content.ReadAsStringAsync();

        //Assert
        Assert.Multiple (()=> 
        {
            Assert.AreEqual(HttpStatusCode.Created, blankResponse.StatusCode);
            Assert.NotNull(responseBody);
        });
    }


    //===>LOG IN
    [Test]
    public async Task LoginCorrectCredentials_returnsOK()
    {
        var testUser = new User
        {
            NickName = "Some1",
            Password = "Password"
        };

        var loginResponse = await _libraryHttpService.LogIn(testUser);
        var responseBody = await loginResponse.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
            Assert.NotNull(responseBody);
        });
    }

    [Test]
    public async Task LoginCorrectNickNonCorrectPassword_returnsBadRequest()
    {
        var testUser = new User
        {
            NickName = "Some1",
            Password = "Wrong"
        };

        var loginResponse = await _libraryHttpService.LogIn(testUser);
        var responseBody = await loginResponse.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, loginResponse.StatusCode);
            Assert.NotNull(responseBody);
        });
    }

    [Test]
    public async Task LoginNonCorrectNickCorrectPassword_returnBadRequest()
    {
        var testUser = new User
        {
            NickName = "Wrong",
            Password = "Password"
        };

        var loginResponse = await _libraryHttpService.LogIn(testUser);
        var responseBody = await loginResponse.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, loginResponse.StatusCode);
            Assert.NotNull(responseBody);
        });
    }

    [Test]
    public async Task LogonWithWrongCredentials_returnBadRequest()
    {
        var testUser = new User
        {
            NickName = "Wrong",
            Password = "Wrong"
        };

        var loginResponse = await _libraryHttpService.LogIn(testUser);
        var responseBody = await loginResponse.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, loginResponse.StatusCode);
            Assert.NotNull(responseBody);
        });
    }
}
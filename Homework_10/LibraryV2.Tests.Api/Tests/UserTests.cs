using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity.Data;
using LibraryV2.Services;
using System.Runtime.CompilerServices;


namespace LibraryV2.Tests.Api.Tests;

public class UsersTests : LibraryV2TestFixture
{
    
    private LibraryHttpService _libraryHttpService;
    private User _testUser;
    private string _token;
    private string _time = DateTime.Now.ToString("yyyyMMddHHmmss");

    [SetUp]
    public async Task Setup()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");

        User _testUser = new User
        {
            FullName = "Papadzilla J. Martinson",
            NickName = "Some1",
            Password = "Password"
        };

        var testUserResponse = await _libraryHttpService.CreateUser(_testUser);
        var loginResponse = await _libraryHttpService.LogIn(_testUser);
        _token = (await loginResponse.Content.ReadAsStringAsync()).Trim('"');
        TestContext.WriteLine($"Token: {_token}");
    }


    //    //TODO cover with tests all endpoints from Users controller

    //===> CREATE USER
    [Test]
    public async Task CreateUser()
    {
        //create new instance of User
        var user = new User
        {
            FullName = "Papadzilla",
            NickName = "Some2" + _time,//unique nickname for each test
            Password = "Password"
        };

        //send request to create new user
        var response = await _libraryHttpService.CreateUser(user);

        //Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        TestContext.WriteLine($"Created Profile Status Code: {response.StatusCode}");

        var createdUser = await response.Content.ReadFromJsonAsync<User>();
        Assert.NotNull(createdUser);
        Assert.AreEqual(user.FullName, createdUser.FullName);
        TestContext.WriteLine($"User Full name: {createdUser.FullName}");
        Assert.AreEqual(user.NickName, createdUser.NickName);
        TestContext.WriteLine($"User Nickname: {createdUser.NickName}");
        Assert.AreEqual(user.Password, createdUser.Password);
        TestContext.WriteLine($"User password: {createdUser.Password}");
    }

    //===> CREATE USER WITH DUPLICATE NACKNAME
    [Test]
    public async Task CreateUserWithDuplicateName()
    {
        //create new user with duplicate username:
        var existedUser = new User
        {
            FullName = "NewName",
            NickName = "Some1",//existing nickname
            Password = "Password"
        };

        //send request to create new user
        var duplicateResponse = await _libraryHttpService.CreateUser(existedUser);

        //Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, duplicateResponse.StatusCode);
        TestContext.WriteLine($"Response Status Code: {duplicateResponse.StatusCode}");

        var createdUser2 = await duplicateResponse.Content.ReadAsStringAsync();
        TestContext.WriteLine($"Response Content: {createdUser2}");
        Assert.NotNull(createdUser2);
    }

    //===> CREATE USER WITH BLANK NACKNAME
    [Test]
    public async Task CreateUserWithBlankName()
    {
        //create new user with blank username:
        var blankUser = new User
        {
            FullName = "NewName",
            NickName = "",//empty nickname
            Password = "Password"
        };

        //send request to create new user
        var blankResponse = await _libraryHttpService.CreateUser(blankUser);

        //Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, blankResponse.StatusCode);
        TestContext.WriteLine($"Response Status Code: {blankResponse.StatusCode}");

        var createdUser2 = await blankResponse.Content.ReadAsStringAsync();
        TestContext.WriteLine($"Response Content: {createdUser2}");
        Assert.NotNull(createdUser2);
    }

    //===> CREATE USER WITH SPACED NACKNAME
    [Test]
    public async Task CreateUserWithSpacedName()
    {
        //create new user with spaced username:
        var spacedUser = new User
        {
            FullName = "NewName",
            NickName = "   ",//3 (three) spaces
            Password = "Password"
        };

        //send request to create new user
        var spacedResponse = await _libraryHttpService.CreateUser(spacedUser);

        //Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, spacedResponse.StatusCode);
        TestContext.WriteLine($"Response Status Code: {spacedResponse.StatusCode}");

        var createdUser2 = await spacedResponse.Content.ReadAsStringAsync();
        TestContext.WriteLine($"Response Content: {createdUser2}");
        Assert.NotNull(createdUser2);
    }

    //===> CREATE USER WITH BLANK PASSWORD
    [Test]
    public async Task CreateUserWithBlankPassword()
    {
        //create new user with blank password:
        var blankPassword = new User
        {
            FullName = "NewName",
            NickName = "test" + _time,//unique nickname for each test
            Password = ""//empty password
        };

        //send request to create new user with blank password
        var blankResponse = await _libraryHttpService.CreateUser(blankPassword);

        //Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, blankResponse.StatusCode);
        TestContext.WriteLine($"Response Status Code: {blankResponse.StatusCode}");

        var createdUser2 = await blankResponse.Content.ReadAsStringAsync();
        TestContext.WriteLine($"Response Content: {createdUser2}");
        Assert.NotNull(createdUser2);

    }

    //===> CREATE USER WITH BLANK FULLNAME
    [Test]
    public async Task CreateUserWithBlankFullName()
    {
        //create new user with blank fullName:
        var blankFullName = new User
        {
            FullName = "",//empty fullName
            NickName = "test" + _time,//unique nickname for each test
            Password = "Password"
        };

        //send request to create new user with blank fullName
        var blankResponse = await _libraryHttpService.CreateUser(blankFullName);

        //Assert
        Assert.AreEqual(HttpStatusCode.Created, blankResponse.StatusCode);
        var createdUser2 = await blankResponse.Content.ReadAsStringAsync();
        TestContext.WriteLine($"Response Content: {createdUser2}");
        Assert.NotNull(createdUser2);
    }


    //===>LOG IN
    [Test]

    [TestCase("Some1", "Password")]
    [TestCase("Some1", "Wrong")]
    [TestCase("Wrong","Password")]
    [TestCase("Wrong", "Wrong")]
    public async Task LogIn(string nickName, string password)
    {
        //create new instance of User
        var testProfile = new User
        {
            NickName = nickName,
            Password = password
        };

        //send request to login
        var LoginResponse = await _libraryHttpService.LogIn(testProfile);

        //asserts
        if (nickName == "Some1" && password == "Password")
        {
            Assert.AreEqual(HttpStatusCode.OK, LoginResponse.StatusCode);
            TestContext.WriteLine($"\nLogin profile Status Code: {LoginResponse.StatusCode}");
            Assert.NotNull(LoginResponse);

            var answerContext = await LoginResponse.Content.ReadAsStringAsync();
            TestContext.WriteLine($"Response Content: {answerContext}");
            TestContext.WriteLine($"Expected username: {nickName} \nactual username: {testProfile.NickName}");
            TestContext.WriteLine($"Expected password: {password} \nactusl password: {testProfile.Password}");
        }
        else
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, LoginResponse.StatusCode);
            TestContext.WriteLine($"\nLogin profile Status Code: {LoginResponse.StatusCode}");
            Assert.NotNull(LoginResponse);

            var answerContext = await LoginResponse.Content.ReadAsStringAsync();
            TestContext.WriteLine($"Response Content: {answerContext}");
            TestContext.WriteLine($"Expected username: {nickName} \nactual username: {testProfile.NickName}");
            TestContext.WriteLine($"Expected password: {password} \nactusl password: {testProfile.Password}");
        }
    }
}
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity.Data;


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
    
    //===> Create user
    [Test]
    
    public async Task CreateUser()
    {
        // Arrange
        var user = new User
        {
            FullName = "Papadzilla J. Martinson",
            NickName = "Some1",
            Password = "Password"
        };

        // Act
        var response = await _libraryHttpService.CreateUser(user);
        
        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        TestContext.WriteLine($"Response Status Code: {response.StatusCode}");
        var createdUser = await response.Content.ReadFromJsonAsync<User>();
        Assert.NotNull(createdUser);
        Assert.AreEqual(user.FullName, createdUser.FullName);
        TestContext.WriteLine($"User Full name:     {createdUser.FullName}");
        Assert.AreEqual(user.NickName, createdUser.NickName);
        TestContext.WriteLine($"User Nickname:      {createdUser.NickName}");
        Assert.AreEqual(user.Password, createdUser.Password);
        TestContext.WriteLine($"User password:      {createdUser.Password}");
    }


    //===> Create user with the same username
    [Test]
    public async Task CreateUserWithDuplicateName ()
    {
        //add new user to db
        var user = new User
        {
            FullName = "Papadzilla J. Martinson",
            NickName = "Some1",
            Password = "Password"
        };

        var response = await _libraryHttpService.CreateUser(user);

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        TestContext.WriteLine($"Response Status Code: {response.StatusCode}");

        var createdUser = await response.Content.ReadFromJsonAsync<User>();
        Assert.NotNull(createdUser);
        Assert.AreEqual(user.FullName, createdUser.FullName);
        TestContext.WriteLine($"User Full name:     {createdUser.FullName}");
        Assert.AreEqual(user.NickName, createdUser.NickName);
        TestContext.WriteLine($"User Nickname:      {createdUser.NickName}");
        Assert.AreEqual(user.Password, createdUser.Password);
        TestContext.WriteLine($"User password:      {createdUser.Password}");

        //create new user with the same username:
        var existedUser = new User
        {
            FullName = "NewName",
            NickName = "some1",
            Password = "Password"
        };

        var duplicateResponse = await _libraryHttpService.CreateUser(existedUser);

        Assert.AreEqual(HttpStatusCode.Created, duplicateResponse.StatusCode);
        TestContext.WriteLine($"Response Status Code: {duplicateResponse.StatusCode}");
        var errorMessage = await duplicateResponse.Content.ReadAsStringAsync();
        TestContext.WriteLine($"Response Content: {errorMessage}");

        var createdUser2 = await duplicateResponse.Content.ReadFromJsonAsync<User>();
        Assert.NotNull(createdUser2);
        Assert.AreEqual(user.FullName, createdUser2.FullName);
        TestContext.WriteLine($"User Full name:     {createdUser2.FullName}");
        Assert.AreEqual(user.NickName, createdUser2.NickName);
        TestContext.WriteLine($"User Nickname:      {createdUser2.NickName}");
        Assert.AreEqual(user.Password, createdUser.Password);
        TestContext.WriteLine($"User password:      {createdUser2.Password}");
    }

    //===>Log In
    [Test]

    public async Task LogIn()
    {
        //create user
        var user = new User
        {
            FullName = "Papadzilla J. Martinson",
            NickName = "Some1",
            Password = "Password"
        };

        //add user to library
        var response = await _libraryHttpService.CreateUser(user);

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        TestContext.WriteLine($"Response Status Code: {response.StatusCode}");
        var createdUser = await response.Content.ReadFromJsonAsync<User>();
        Assert.NotNull(createdUser);
        Assert.AreEqual(user.FullName, createdUser.FullName);
        TestContext.WriteLine($"User Full name:     {createdUser.FullName}");
        Assert.AreEqual(user.NickName, createdUser.NickName);
        TestContext.WriteLine($"User Nickname:      {createdUser.NickName}");
        Assert.AreEqual(user.Password, createdUser.Password);
        TestContext.WriteLine($"User password:      {createdUser.Password}");

        //create new user to check login
        var loginRequest = new User
        {
            NickName = "Some1",
            Password = "Password"
        };

        var newResponse = await _libraryHttpService.LogIn(loginRequest);

        Assert.AreEqual(HttpStatusCode.OK, newResponse.StatusCode);
        TestContext.WriteLine($"Response Status Code: {newResponse.StatusCode}");

        var loginUser = await newResponse.Content.ReadAsStringAsync();
        TestContext.WriteLine($"User in db:    {user.NickName}\nLogined user:  {loginRequest.NickName}");
        TestContext.WriteLine($"Users psaaword in db:   {user.Password}\nLogined user password:  {loginRequest.Password}");
    }
    //===>Login not registred user
    [Test]

    public async Task LoginNotRegistredUser()
    {
        //create user
        var user = new User
        {
            FullName = "Papadzilla J. Martinson",
            NickName = "Some1",
            Password = "Password"
        };

        //add user to library
        var response = await _libraryHttpService.CreateUser(user);

        //Check if in DB
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        TestContext.WriteLine($"Response Status Code: {response.StatusCode}");
        var createdUser = await response.Content.ReadFromJsonAsync<User>();
        Assert.NotNull(createdUser);
        Assert.AreEqual(user.FullName, createdUser.FullName);
        TestContext.WriteLine($"User Full name:     {createdUser.FullName}");
        Assert.AreEqual(user.NickName, createdUser.NickName);
        TestContext.WriteLine($"User Nickname:      {createdUser.NickName}");
        Assert.AreEqual(user.Password, createdUser.Password);
        TestContext.WriteLine($"User password:      {createdUser.Password}");


        //create new who not registred user to check login
        var doesntExistUser = new User
        {
            NickName = "ImRobot",
            Password = "NoPassword"
        };
       
        //Act
        var responseLogin = await _libraryHttpService.LogIn(doesntExistUser);

        // Asserts
        Assert.AreEqual(HttpStatusCode.BadRequest, responseLogin.StatusCode);
        TestContext.WriteLine($"Response Status Code: {responseLogin.StatusCode}");
        Assert.NotNull(responseLogin);

        //text context of request
        var loginUser = await responseLogin.Content.ReadAsStringAsync();
        TestContext.WriteLine($"User in db:    {user.NickName}\nTry to login:  {doesntExistUser.NickName}");
        TestContext.WriteLine($"Users password in db:   {user.Password}\nTry to login password:  {doesntExistUser.Password}");

    }
}
using System.Data;
using System.Net.Http.Json;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;

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
    public async Task CreateUserSusses()
    {
        var time = DateTime.Now.Ticks;
        User user = new()
        {
            FullName = "Robert Finch" + time,
            Password = "Qwerty",
            NickName = "Finch" + time
        };

        User? response = await _libraryHttpService.CreateUser(user);

        Assert.Multiple(()=>{
            Assert.That(response, Is.Not.Null);
            Assert.That(response.FullName, Is.EqualTo(user.FullName));
            Assert.That(response.NickName, Is.EqualTo(user.NickName));
            Assert.That(response.Password, Is.EqualTo(user.Password));
        });
    }

    [Test]
    public async Task LoginUser(){

        var time = DateTime.Now.Ticks;
        User user = new()
        {
            FullName = "Robert Finch" + time,
            Password = "Qwerty",
            NickName = "Finch" + time
        };
        await _libraryHttpService.CreateUser(user);
        var response = await _libraryHttpService.LogIn(user);

        Assert.Multiple(()=>{
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Token, Is.Not.Empty);
        });
    }
}
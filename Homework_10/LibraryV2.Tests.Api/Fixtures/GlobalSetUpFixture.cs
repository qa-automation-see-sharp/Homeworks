using System.Net.Http.Json;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Fixtures;

[SetUpFixture]
public class GlobalSetUpFixture
{
    protected readonly LibraryHttpService _libraryHttpService = new();
    
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _libraryHttpService.Configure("http://localhost:5111/");
        
        await _libraryHttpService.CreateDefaultUser();
        
        /*for (var i = 0; i < 3; i++)
        {
            var user = new User
            {
                FullName = "User" + i,
                NickName = "User" + i,
                Password = "Password" + i
            };
            await _libraryHttpService.CreateUser(user);
            var httpResponseMessage = await _libraryHttpService.LogIn(user);
            var token = await httpResponseMessage.Content.ReadAsStringAsync();
            token = token.Trim('"');
            _users.Add(user, token);
        }*/
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
    }
}
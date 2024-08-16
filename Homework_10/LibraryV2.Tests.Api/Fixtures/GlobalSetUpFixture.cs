using System.Net.Http.Json;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Fixtures;

[SetUpFixture]
public class GlobalSetUpFixture
{
    public readonly LibraryHttpService _libraryHttpService = new ();
    protected readonly Dictionary<User, string> _users = new();
    
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _libraryHttpService.Configure("http://localhost:5111/");
        
        for (var i = 0; i < 3; i++)
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
        }
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine("Here is the one-time tear down");
    }
}
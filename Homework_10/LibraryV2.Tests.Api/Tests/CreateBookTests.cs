using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

// Not All tests scenarios are covered in this test class 
public class CreateBookTests : LibraryV2TestFixture
{
    // if this setup is not needed, remove it
    [SetUp]
    public new void SetUp()
    {
    }
    
    // 
    [Test]
    public async Task CreateBookAsync()
    {
        var book = new Book
        {
            Author = Guid.NewGuid().ToString(),
            Title = Guid.NewGuid().ToString(),
            YearOfRelease = 2023
        };

        var userToken = Users.First().Value;

        var response = await LibraryHttpService.CreateBook(userToken.Token, book);
        var jsonString = await response.Content.ReadAsStringAsync();
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }
}
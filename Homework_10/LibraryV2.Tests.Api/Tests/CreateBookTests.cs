using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    [SetUp]
    public new void SetUp()
    {
    }

    [Test]
    public async Task CreateBookAsync()
    {
        var book = new Book
        {
            Author = "Taylor Jenkins Rid",
            Title = "Evelin Hugo",
            YearOfRelease = 2023
        };

        var response = await _libraryHttpService.CreateBook(_users.First().Value, book);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }
}
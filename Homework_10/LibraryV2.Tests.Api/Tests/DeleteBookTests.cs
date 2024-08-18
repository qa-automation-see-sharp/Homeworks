using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService = new();

    [SetUp]
    public async new Task SetUp()
    {
        var client = _libraryHttpService.Configure("http://localhost:5111/");
        await client.CreateDefaultUser();
        await client.Authorize();
    }

    //TODO cover with tests all endpoints from Books controller
    // Delete book

    [Test]
    public async Task DeleteBook()
    {
        var newBook = new Book()
        {
            Title = "ToDelete",
            Author = "None"
        };

        var bookCreated = await _libraryHttpService.CreateBook(newBook);

        HttpResponseMessage response = await _libraryHttpService.DeleteBook(newBook.Title, newBook.Author);
        
        var jsonString = await response.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode); 
            Assert.That(jsonString.Contains("ToDelete by None deleted"));
        });
    }
}
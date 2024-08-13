using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    [SetUp]
    public new void SetUp()
    {
        LibraryHttpService = new LibraryHttpService();
        LibraryHttpService.Configure("http://localhost:5111/");
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

        var bookCreated = await LibraryHttpService.CreateBook(Token, newBook);

        HttpResponseMessage response = await LibraryHttpService.DeleteBook(Token, newBook.Title, newBook.Author);
        
        var jsonString = await response.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode); 
            Assert.That(jsonString.Contains("ToDelete by None deleted"));
        });
    }
}
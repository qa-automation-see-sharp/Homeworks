using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;

    [SetUp]
    public new void SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }

    [Test]
    public async Task DeleteBook()
    {
        var newBook = new Book()
        {
            Title = "TitleToDelete",
            Author = "AuthorToDelete"
        };

        var token = "";

        var bookCreated = await _libraryHttpService.CreateBook(token, newBook);

        HttpResponseMessage response = await _libraryHttpService.DeleteBook(token, newBook.Title, newBook.Author);

        var jsonString = await response.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.Equals(HttpStatusCode.OK, response.StatusCode);
        });
        //TODO cover with tests all endpoints from Books controller
        // Delete book
    }
}
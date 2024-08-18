using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    //TODO cover with tests all endpoints from Books controller
    // Delete book
    private Book _book {get; set;}
    [SetUp]
    public async Task SetUp()
    {
        _book = new()
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = 1980
        };
        var client = _httpService.Configure("http://localhost:5111/");
        await client.CreateDefaultUser();
        await client.Authorize();
        await client.CreateBook(_book);
    }

    [Test]
    public async Task DeleteBook()
    {
        var response = await _httpService.DeleteBook(_book.Title, _book.Author);
        var jsonString = await response.Content.ReadAsStringAsync();
        var s = jsonString.Trim('"');
        Assert.That(s.Equals($"{_book.Title} by {_book.Author} deleted"));
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test]
    public async Task DeleteNotFoundBook()
    {
        var title = "Happy";
        var author = "Chan";
        var response = await _httpService.DeleteBook(title, author);
        var jsonString = await response.Content.ReadAsStringAsync();
        var s = jsonString.Trim('"');
        Assert.That(s.Equals($"Book :{title} by {author} not found"));
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

     [Test]
    public async Task DeleteBookUnauthorized()
    {
        var response = await _httpService.DeleteBook(_book.Title, _book.Author, "123");
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
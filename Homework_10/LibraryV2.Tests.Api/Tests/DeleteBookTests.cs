using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    //TODO cover with tests all endpoints from Books controller
    // Delete book
    [Test]
    public async Task DeleteBook()
    {
        var token = _users.First().Value;
        var book = _books.First();
        string? response = await _libraryService.DeleteBook(token, book.Title, book.Author);

        Assert.That(response.Equals($"{book.Title} by {book.Author} deleted"));
    }
}
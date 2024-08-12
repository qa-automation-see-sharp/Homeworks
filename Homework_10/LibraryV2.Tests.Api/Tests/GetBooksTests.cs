using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    [Test]
    public async Task GetBooksByTitle()
    {
        var book = _books.First();
        List<Book> response = await _libraryService.GetBooksByTitle(book.Title);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response[0].Title, Is.EqualTo(book.Title));
            Assert.That(response[0].Author, Is.EqualTo(book.Author));
            Assert.That(response[0].YearOfRelease, Is.EqualTo(book.YearOfRelease));
        });
    }

    [Test]
    public async Task GetBooksByAuthor()
    {
        var book = _books.First();
        List<Book> response = await _libraryService.GetBooksByAuthor(book.Author);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response[0].Title, Is.EqualTo(book.Title));
            Assert.That(response[0].Author, Is.EqualTo(book.Author));
            Assert.That(response[0].YearOfRelease, Is.EqualTo(book.YearOfRelease));
        });
    }
}
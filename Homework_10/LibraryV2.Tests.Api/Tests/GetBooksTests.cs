using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    [Test]
    public async Task GetBooksByTitle()
    {
        var book = _books.First();
        List<Book> response = await _libraryHttpService.GetBooksByTitle(book.Title);

        Assert.Multiple( () => 
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response[0].Title, Is.EqualTo(book.Title));
            Assert.That(response[0].Author, Is.EqualTo(book.Author));
            Assert.That(response[0].YearOfRelease, Is.EqualTo(book.YearOfRelease));
        });
    }
    private LibraryHttpService _libraryHttpService;

    [SetUp]
    public void SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }
}
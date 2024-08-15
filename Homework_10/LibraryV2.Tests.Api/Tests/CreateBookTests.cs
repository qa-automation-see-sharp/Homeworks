using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    private Book _book;

    public async Task CreateBook(string title, string author, int year)
    {
        _book = new Book()
        {
            Title = title,
            Author = author,
            YearOfRelease = year
        };

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Title, Is.EqualTo(_book.Title));
            Assert.That(response.Author, Is.EqualTo(_book.Author));
            Assert.That(response.YearOfRelease, Is.EqualTo(_book.YearOfRelease));

        });
    }
    [SetUp]
    public new void SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }

    //TODO cover with tests all endpoints from Books controller
    // Create book
}
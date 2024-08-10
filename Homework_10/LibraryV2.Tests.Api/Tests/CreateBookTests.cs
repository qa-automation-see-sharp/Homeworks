using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;
    public string Token { get; private set; }

    [SetUp]
    public new void SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
        var user = new User()
        {
            NickName = "locksmith",
            Password = "cg2ir37"
        };

        Token = _libraryHttpService.LogIn(user).GetAwaiter().GetResult().Token.Trim('"');
    }

    //TODO cover with tests all endpoints from Books controller
    // Create book

    [Test]
    public async Task CreateBook()
    {
        var newBook = new Book()
        {
            Title = "Grasshopper",
            Author = "Kotaro Isaka",
            YearOfRelease = 2004
        };

        Book? response = await _libraryHttpService.CreateBook(Token, newBook);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Title, Is.EqualTo(newBook.Title));
            Assert.That(response.Author, Is.EqualTo(newBook.Author));
            Assert.That(response.YearOfRelease, Is.EqualTo(newBook.YearOfRelease));
        });
    }
}
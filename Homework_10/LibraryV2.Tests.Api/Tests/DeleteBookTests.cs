using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json.Linq;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
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
    // Delete book

    [Test]
    public async Task DeleteBook()
    {
        var newBook = new Book()
        {
            Title = "ToDelete",
            Author = "None"
        };

        Book? bookCreated = await _libraryHttpService.CreateBook(Token, newBook);

        string? response = await _libraryHttpService.DeleteBook(Token, newBook.Title, newBook.Author);

        Assert.Multiple(() =>
        {
            Assert.That(response.Equals("ToDelete by None deleted")); 
        });
    }
}
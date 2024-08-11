using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;
    private string _token {get; set;}
    private long _time;
    private Book _book;

    [SetUp]
    public new void SetUp()
    {
        _time = DateTime.Now.Ticks;
        User _user = new()
        {
            FullName = "Robert Finch" + _time,
            Password = "Qwerty",
            NickName = "Finch" + _time
        };
        _book = new()
        {
            Title = "Harry Potter Chamber of Secrets " + _time,
            Author = "Joanne Rowling",
            YearOfRelease = 1998
        };

        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
       _libraryHttpService.CreateUser(_user).GetAwaiter().GetResult();
        _token = _libraryHttpService.LogIn(_user).GetAwaiter().GetResult().Token.Trim('"');
        _libraryHttpService.CreateBook(_token,_book).GetAwaiter().GetResult();
    }

    //TODO cover with tests all endpoints from Books controller
    // Delete book
    [Test]
    public async Task DeleteBook()
    {
        string? response = await _libraryHttpService.DeleteBook(_token, _book.Title, _book.Author);

        Assert.That(response.Equals($"{_book.Title} by {_book.Author} deleted"));
    }
}
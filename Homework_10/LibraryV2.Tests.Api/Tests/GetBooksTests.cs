using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;
    private string _token {get; set;}
    private long _time = DateTime.Now.Ticks;

    private Book _book;

    [SetUp]
    public void SetUp()
    {
        User _user = new(){
            FullName = "Robert Finch" + _time,
            Password = "Qwerty",
            NickName = "Finch" + _time
        };
        _book = new()
        {
            Title = "Harry Potter Chamber of Secrets" + _time,
            Author = "Joanne Rowling" + _time,
            YearOfRelease = 1998
        };
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
        _libraryHttpService.CreateUser(_user).GetAwaiter().GetResult();
        _token = _libraryHttpService.LogIn(_user).GetAwaiter().GetResult().Token.Trim('"');
        _libraryHttpService.CreateBook(_token,_book).GetAwaiter().GetResult();
    }

    [Test]
    public async Task GetBooksByTitle()
    {
        List<Book> response = await _libraryHttpService.GetBooksByTitle(_book.Title);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response[0].Title, Is.EqualTo(_book.Title));
            Assert.That(response[0].Author, Is.EqualTo(_book.Author));
            Assert.That(response[0].YearOfRelease, Is.EqualTo(_book.YearOfRelease));
        });
    }

    [Test]
    public async Task GetBooksByAuthor()
    {
        List<Book> response = await _libraryHttpService.GetBooksByAuthor(_book.Author);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response[0].Title, Is.EqualTo(_book.Title));
            Assert.That(response[0].Author, Is.EqualTo(_book.Author));
            Assert.That(response[0].YearOfRelease, Is.EqualTo(_book.YearOfRelease));
        });
    }
}
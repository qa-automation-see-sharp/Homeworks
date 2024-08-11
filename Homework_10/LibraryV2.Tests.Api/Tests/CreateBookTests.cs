using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;
    private string _token {get; set;}
    private long _time;
    [SetUp]
    public new void SetUp()
    {
        _time = DateTime.Now.Ticks;
        User user = new()
        {
            FullName = "Robert Finch" + _time,
            Password = "Qwerty",
            NickName = "Finch" + _time
        };
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
        _libraryHttpService.CreateUser(user).GetAwaiter().GetResult();
        _token = _libraryHttpService.LogIn(user).GetAwaiter().GetResult().Token.Trim('"');
    }
    
    [Test]
    [TestCase("Philosopher's Stone", "Joanne Rowling", 1997)]
    [TestCase("Chamber of Secrets", "Joanne Rowling", 1998)]
    [TestCase("Prisoner of Azkaban", "Joanne Rowling", 1999)]
    [TestCase("Goblet of Fire ", "Joanne Rowling", 2000)]
    [TestCase("Order of the Phoenix", "Joanne Rowling", 2003)]
    [TestCase("Half-Blood Prince", "Joanne Rowling", 2005)]
    public async Task CreateBook(string title, string author, int year){
        Book book = new(){
            Title = title + _time,
            Author = author,
            YearOfRelease = year
        };
        
        var response = await _libraryHttpService.CreateBook(_token, book);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Title, Is.EqualTo(book.Title));
            Assert.That(response.Author, Is.EqualTo(book.Author));
            Assert.That(response.YearOfRelease, Is.EqualTo(book.YearOfRelease));
        });
    }
}
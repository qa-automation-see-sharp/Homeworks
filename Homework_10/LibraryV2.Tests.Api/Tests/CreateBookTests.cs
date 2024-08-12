using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    private Book _book;

    [TestCase("Philosopher's Stone", "Joanne Rowling", 1997)]
    [TestCase("Chamber of Secrets", "Joanne Rowling", 1998)]
    [TestCase("Prisoner of Azkaban", "Joanne Rowling", 1999)]
    [TestCase("Goblet of Fire ", "Joanne Rowling", 2000)]
    [TestCase("Order of the Phoenix", "Joanne Rowling", 2003)]
    [TestCase("Half-Blood Prince", "Joanne Rowling", 2005)]
    public async Task CreateBook(string title, string author, int year)
    {
        _book = new()
        {
            Title = title,
            Author = author,
            YearOfRelease = year
        };

        var response = await _libraryService.CreateBook(_users.First().Value, _book);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Title, Is.EqualTo(_book.Title));
            Assert.That(response.Author, Is.EqualTo(_book.Author));
            Assert.That(response.YearOfRelease, Is.EqualTo(_book.YearOfRelease));
        });
    }

    [TearDown]
    public new async Task DeleteBook()
    {
        var token = _users.First().Value;
        await _libraryService.DeleteBook(token, _book.Title, _book.Author);
    }
}
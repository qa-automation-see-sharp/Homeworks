using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    private Book _book;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var client = _httpService.Configure("http://localhost:5111/");
        await client.CreateDefaultUser();
        await client.Authorize();
    }
    
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

        var obj = await _httpService.CreateBook(_book);
        var response = await obj.Content.ReadAsStringAsync();
        var bookObj = JsonConvert.DeserializeObject<Book>(response);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.Created, obj.StatusCode);
            Assert.That(response, Is.Not.Null);
            Assert.That(bookObj.Title, Is.EqualTo(_book.Title));
            Assert.That(bookObj.Author, Is.EqualTo(_book.Author));
            Assert.That(bookObj.YearOfRelease, Is.EqualTo(_book.YearOfRelease));
        });
    }

    [TearDown]
    public new async Task DeleteBook()
    {
        await _httpService.DeleteBook(_book.Title, _book.Author);
    }
}
<<<<<<< HEAD
using System.Net;
=======
>>>>>>> 25dbe55074a0fcb5bdb97f6c647ef67b46fb8674
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Tests;

public class GetBooksTests : LibraryV2TestFixture
{
<<<<<<< HEAD
    private readonly LibraryHttpService _httpService = new();
=======
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
>>>>>>> 25dbe55074a0fcb5bdb97f6c647ef67b46fb8674

    [OneTimeSetUp]
    public new async Task OneTimeSetUp()
    {
        var client = _httpService.Configure("http://localhost:5111/");
        await _httpService.CreateDefaultUser();
        await client.Authorize();
    }

    [Test]
    public async Task GetBooksByTitle200()
    {
        var book = new Book 
        {
            Title = "title",
            Author = "author",
            YearOfRelease = 0000
        };
        
        await _httpService.CreateBook(book);
        var httpResponseMessage = await _httpService.GetBooksByTitle(book.Title);
        var content = await httpResponseMessage.Content.ReadAsStringAsync();       
        var bookFromResponse = JsonConvert.DeserializeObject<List<Book>>(content);

        Assert.Multiple(() =>
        {
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(bookFromResponse[0].Title, Is.EqualTo("title"));
            Assert.That(bookFromResponse[0].Author, Is.EqualTo("author"));
            Assert.That(bookFromResponse[0].YearOfRelease, Is.EqualTo(book.YearOfRelease));
        });
    }

    [Test]
    public async Task GetBooksByAuthor200()
    {
        var book = new Book 
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = 0000
        };

        await _httpService.CreateBook(book);
        var httpResponseMessage = await _httpService.GetBooksByAuthor(book.Author);
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var bookFromResponse = JsonConvert.DeserializeObject<List<Book>>(content);

        Assert.Multiple(() =>
        {
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(bookFromResponse[0].Title, Is.EqualTo(book.Title));
            Assert.That(bookFromResponse[0].Author, Is.EqualTo(book.Author));
            Assert.That(bookFromResponse[0].YearOfRelease, Is.EqualTo(book.YearOfRelease));
        });
    }
}
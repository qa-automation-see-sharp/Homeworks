using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Tests;

public class GetBooksTests : LibraryV2TestFixture
{
    private readonly LibraryHttpService _httpService = new();

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
        //Тут ми створюємо постійно нову книгу, щоб кожного разу тест був з новою книгою та був зеленим.
        var book = new Book
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = 0000
        };

        await _httpService.CreateBook(book);
        var httpResponseMessage = await _httpService.GetBooksByTitle(book.Title);
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

    [Test]
    public async Task GetBooksByTitle400()
    {
        // А тут ми шукаємо книгу по заголовку, якого немає в базі
        var httpResponseMessage = await _httpService.GetBooksByTitle("title");

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
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

    [Test]
    public async Task GetBooksByAuthor400()
    {
        var httpResponseMessage = await _httpService.GetBooksByAuthor("author");

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
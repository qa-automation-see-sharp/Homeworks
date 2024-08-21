using Bogus;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Helpers;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    private readonly LibraryHttpService _libraryHttpService = new();
    
    [OneTimeSetUp]
    public new async Task OneTimeSetUp()
    {
        var client = _libraryHttpService.Configure("http://localhost:5111/");
        await client.CreateDefaultUser();
        await client.Authorize();
    }

    [Test]
    public async Task CreateBook_WhenDataIsValid_ReturnCreated()
    {
        var book = DataHelper.CreateBook();

        HttpResponseMessage response = await _libraryHttpService.CreateBook(book);

        var jsonString = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<Book>(jsonString);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(books.Title, Is.EqualTo(book.Title));
            Assert.That(books.Author, Is.EqualTo(book.Author));
            Assert.That(books.YearOfRelease, Is.EqualTo(book.YearOfRelease));
        });
    }

    [Test]
    public async Task CreateBook_WhenTokenIsInvalid_ReturnUnauthorized()
    {
        var book = DataHelper.CreateBook();

        HttpResponseMessage response = await _libraryHttpService.CreateBook("invalid", book);

        var jsonString = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<Book>(jsonString);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        });
    }
}
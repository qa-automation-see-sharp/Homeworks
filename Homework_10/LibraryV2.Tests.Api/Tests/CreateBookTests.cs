using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
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
    public async Task CreateBook201()
    {
        var book = new Book
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = 0000
        };

        var httpResponseMessage = await _httpService.CreateBook(book);
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var bookFromResponse = JsonConvert.DeserializeObject<Book>(content);

        Assert.Multiple(() =>
        {
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(bookFromResponse.Title, Is.EqualTo(book.Title));
            Assert.That(bookFromResponse.Author, Is.EqualTo(book.Author));
            Assert.That(bookFromResponse.YearOfRelease, Is.EqualTo(book.YearOfRelease));
        });
    }

    [Test]
    public async Task CreateBook400()
    {
        var book = new Book
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = 0000
        };

        await _httpService.CreateBook(book);
        var httpResponseMessage = await _httpService.CreateBook(book);

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task CreateBook401()
    {
        var book = new Book
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = 0000
        };

       var httpResponseMessage = await _httpService.CreateBook("token", book);

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
}
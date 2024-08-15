using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using static LibraryV2.ApiEndpoints;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;

    public Book NewBook { get; private set; }

    [SetUp]
    public new async Task SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");

        Book NewBook = new Book()
        {
            Title = "TestTitle",
            Author = "TestAuthor",
            YearOfRelease = 2000
        };
    }

    [Test]
     
    public async Task CreateBook()
    {
         NewBook = new Book
        {
            Title = "TestUser",
            Author = "TestPassword",
            YearOfRelease = 2000,
        };

        HttpResponseMessage response = await _libraryHttpService.CreateBook(Token, NewBook);

        var jsonStringUser = await response.Content.ReadAsStringAsync();
        var userResponse = JsonConvert.DeserializeObject<Book>(jsonStringUser);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode,Is.EqualTo(HttpStatusCode.Created));
            Assert.That(Book.Title, Is.EqualTo(NewBook.Title));
            Assert.That(book.Author, Is.EqualTo(NewBook.Author));
            Assert.That(books.YearOfRelease, Is.EqualTo(NewBook.YearOfRelease));
        });
    }

    //TODO cover with tests all endpoints from Books controller
    // Create book
}
using Bogus;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    internal LibraryHttpService _libraryHttpService = new();

    public Book NewBook { get; private set; }
    
    [OneTimeSetUp]
    public new async Task OneTimeSetUp()
    {
        var client = _libraryHttpService.Configure("http://localhost:5111/");
        await client.CreateDefaultUser();
        await client.Authorize();
    }
    
    //TODO cover with tests all endpoints from Books controller
    // Create book

    [Test]
    public async Task CreateBook()
    {
        var faker = new Faker();
        NewBook = new Book()
        {
            Title = $"Pragmatic Programmer{faker.Random.AlphaNumeric(4)}",
            Author = "Andrew Hunt",
            YearOfRelease = 1999
        };

        HttpResponseMessage response = await _libraryHttpService.CreateBook(NewBook);

        var jsonString = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<Book>(jsonString);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.That(books.Title, Is.EqualTo(NewBook.Title));
            Assert.That(books.Author, Is.EqualTo(NewBook.Author));
            Assert.That(books.YearOfRelease, Is.EqualTo(NewBook.YearOfRelease));
        });
    }

    [TearDown]
    public new void TearDown()
    {
        var response = _libraryHttpService.DeleteBook(NewBook.Title, NewBook.Author);
    }
}
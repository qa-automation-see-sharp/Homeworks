using Bogus;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    public Book NewBook { get; private set; }

    [SetUp]
    public new void SetUp()
    {
        LibraryHttpService = new LibraryHttpService();
        LibraryHttpService.Configure("http://localhost:5111/");

        var faker = new Faker();

        NewBook = new Book()
        {
            Title = $"Pragmatic Programmer{faker.Random.AlphaNumeric(4)}",
            Author = "Andrew Hunt",
            YearOfRelease = 1999
        };
    }

    //TODO cover with tests all endpoints from Books controller
    // Create book

    [Test]
    public async Task CreateBook()
    {

        HttpResponseMessage response = await LibraryHttpService.CreateBook(Token, NewBook);

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
        var response = LibraryHttpService.DeleteBook(Token, NewBook.Title, NewBook.Author);
    }
}
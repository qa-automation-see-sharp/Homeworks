using Bogus;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    private readonly LibraryHttpService _libraryHttpService = new();
    private Book _newBook;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        var client = _libraryHttpService.Configure("http://localhost:5111/");
        await client.CreateDefaultUser();
        await client.Authorize();

        var faker = new Faker();
        _newBook = new Book
        {
            Author = "Kotaro Isaka",
            Title = $"Grasshopper {faker.Random.AlphaNumeric(4)}",
            YearOfRelease = 2004
        };

        await _libraryHttpService.CreateBook(_newBook);
    }

    [Test]
    public async Task GetBooksByTitle()
    {
        HttpResponseMessage response = await _libraryHttpService.GetBooksByTitle(_newBook.Title);

        var jsonString = await response.Content.ReadAsStringAsync();

       var books = JsonConvert.DeserializeObject<List<Book>>(jsonString);

        Assert.Multiple(() =>
        {
            // Зараз рекомендується використовувати Assert.That замість Assert.AreEqual
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(books.Count, Is.GreaterThan(0));
            Assert.That(books[0].Title, Is.EqualTo(_newBook.Title));
            Assert.That(books[0].Author, Is.EqualTo("Kotaro Isaka"));
            Assert.That(books[0].YearOfRelease, Is.EqualTo(2004));
        });
    }

    [Test]
    public async Task GetBooksByAuthor()
    {
        HttpResponseMessage response = await _libraryHttpService.GetBooksByAuthor("Kotaro Isaka");

        var jsonString = await response.Content.ReadAsStringAsync();

        var books = JsonConvert.DeserializeObject<List<Book>>(jsonString);

        Assert.Multiple(() =>
        {
            // Зараз рекомендується використовувати Assert.That замість Assert.AreEqual
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(books.Count, Is.GreaterThan(0));
            Assert.That(books[0].Title, Is.EqualTo(_newBook.Title));
            Assert.That(books[0].Author, Is.EqualTo("Kotaro Isaka"));
            Assert.That(books[0].YearOfRelease, Is.EqualTo(2004));
        });
    }

    // А тут навпаки потрібен асінхронний метод, щоб точно видалити книгу
    [OneTimeTearDown]
    public new async Task OneTimeTearDown()
    {
        await _libraryHttpService.DeleteBook(_newBook.Title, _newBook.Author);
    }
}
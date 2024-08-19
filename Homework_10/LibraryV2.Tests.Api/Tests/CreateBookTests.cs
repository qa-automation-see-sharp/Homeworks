using Bogus;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    //Якщо змінна існує тільки в рамках цього классу можно залишати її приватною
    private readonly LibraryHttpService _libraryHttpService = new();

    //Якщо змінна не використовуються в класах нащадках, то можна залишити її приватною
    private Book NewBook { get; set; }
    
    [OneTimeSetUp]
    public new async Task OneTimeSetUp()
    {
        var client = _libraryHttpService.Configure("http://localhost:5111/");
        await client.CreateDefaultUser();
        await client.Authorize();
    }

    [Test]
    public async Task CreateBook()
    {
        // Жирний плюс за Фейкер, ще можно використати Guid.NewGuid().ToString() для генерації унікальних значень 
        var faker = new Faker();
        NewBook = new Book
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
            // Зараз рекомендується використовувати Assert.That замість Assert.AreEqual
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(books.Title, Is.EqualTo(NewBook.Title));
            Assert.That(books.Author, Is.EqualTo(NewBook.Author));
            Assert.That(books.YearOfRelease, Is.EqualTo(NewBook.YearOfRelease));
        });
    }

     // А тут навпаки потрібен асінхронний метод, щоб точно видалити книгу
    [TearDown]
    public new async Task TearDown()
    {
        await _libraryHttpService.DeleteBook(NewBook.Title, NewBook.Author);
    }
}
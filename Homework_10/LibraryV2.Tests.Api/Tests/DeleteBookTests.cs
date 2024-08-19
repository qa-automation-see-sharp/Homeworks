using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    // Якщо створюєщь цей обʼєкт один раз у цьому класі, то можна залишити його приватним та рід онлі
    private readonly LibraryHttpService _libraryHttpService = new();

    [SetUp]
    public async Task SetUp()
    {
        var client = _libraryHttpService.Configure("http://localhost:5111/");
        await client.CreateDefaultUser();
        await client.Authorize();
    }

    [Test]
    public async Task DeleteBook()
    {
        var newBook = new Book()
        {
            Title = "ToDelete",
            Author = "None"
        };

        var bookCreated = await _libraryHttpService.CreateBook(newBook);

        HttpResponseMessage response = await _libraryHttpService.DeleteBook(newBook.Title, newBook.Author);

        var jsonString = await response.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            // Зараз рекомендується використовувати Assert.That замість Assert.AreEqual
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonString.Contains("ToDelete by None deleted"));
        });
    }
}
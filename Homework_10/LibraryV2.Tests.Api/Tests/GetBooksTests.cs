using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Models;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    private Book Book { get; set; }

    [OneTimeSetUp]
    public new async Task OneTimeSetUp()
    {
        Book = new()
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = 1980
        };
        await HttpService.CreateBook(Book);
    }

    [Test]
    public async Task GetBooksByTitle()
    {
        var response = await HttpService.GetBooksByTitle(Book.Title);
        var listStringBooks = await response.Content.ReadAsStringAsync();
        var json = JsonConvert.DeserializeObject<List<Book>>(listStringBooks);


        Assert.Multiple(() =>
        {
            // Зараз рекомендують використовувати Assert.That, бо він дає більше інформації про помилку
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(json, Is.Not.Empty);
            Assert.That(response, Is.Not.Null);
            Assert.That(json[0].Title, Is.EqualTo(Book.Title));
            Assert.That(json[0].Author, Is.EqualTo(Book.Author));
            Assert.That(json[0].YearOfRelease, Is.EqualTo(Book.YearOfRelease));
        });
    }

    [Test]
    public async Task GetBooksByAuthor()
    {
        var response = await HttpService.GetBooksByAuthor(Book.Author);
        var listStringBooks = await response.Content.ReadAsStringAsync();
        var json = JsonConvert.DeserializeObject<List<Book>>(listStringBooks);

        Assert.Multiple(() =>
        {
            // Зараз рекомендують використовувати Assert.That, бо він дає більше інформації про помилку
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response, Is.Not.Null);
            Assert.That(json[0].Title, Is.EqualTo(Book.Title));
            Assert.That(json[0].Author, Is.EqualTo(Book.Author));
            Assert.That(json[0].YearOfRelease, Is.EqualTo(Book.YearOfRelease));
        });
    }
}
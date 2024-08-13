using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    [OneTimeSetUp]
    public async Task SetUp()
    {
        LibraryHttpService = new LibraryHttpService();
        LibraryHttpService.Configure("http://localhost:5111/");
    }

    [Test]
    public async Task GetBooksByTitle()
    {
        HttpResponseMessage response = await LibraryHttpService.GetBooksByTitle("Grasshopper");

        var jsonString = await response.Content.ReadAsStringAsync();

       var books = JsonConvert.DeserializeObject<List<Book>>(jsonString);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(books.Count, Is.GreaterThan(0));
            Assert.That(books[0].Title, Is.EqualTo("Grasshopper"));
            Assert.That(books[0].Author, Is.EqualTo("Kotaro Isaka"));
            Assert.That(books[0].YearOfRelease, Is.EqualTo(2004));
        });
    }

    [Test]
    public async Task GetBooksByAuthor()
    {
        HttpResponseMessage response = await LibraryHttpService.GetBooksByAuthor("Kotaro Isaka");

        var jsonString = await response.Content.ReadAsStringAsync();

        var books = JsonConvert.DeserializeObject<List<Book>>(jsonString);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(books.Count, Is.GreaterThan(0));
            Assert.That(books[0].Title, Is.EqualTo("Grasshopper"));
            Assert.That(books[0].Author, Is.EqualTo("Kotaro Isaka"));
            Assert.That(books[0].YearOfRelease, Is.EqualTo(2004));
        });
    }
}
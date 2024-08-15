using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using static LibraryV2.ApiEndpoints;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;

    [SetUp]
    public async Task SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }
    [Test]
    public async Task GetBooksByTitle()
    {
        HttpResponseMessage response = await LibraryHttpService.GetBooksByTitle("TestTitle");

        var jsonString = await response.Content.ReadAsStringAsync();

        var books = JsonConvert.DeserializeObject<List<Book>>(jsonString);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(books.Count, Is.GreaterThan(0));
            Assert.That(books[0].Title, Is.EqualTo("TestTitle"));
            Assert.That(books[0].Author, Is.EqualTo("TestAuthor"));
            Assert.That(books[0].YearOfRelease, Is.EqualTo(2000));
        });
    }

}
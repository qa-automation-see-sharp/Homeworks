using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Models;
using Newtonsoft.Json;
using System.Net;
using LibraryV2.Tests.Api.TestHelpers;
using Microsoft.AspNetCore.Http;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    private Book Book { get; set; }

    [OneTimeSetUp]
    public new async Task OneTimeSetUp()
    {
        Book = DataHelper.BookHelper.RandomBook();
        await HttpService.PostBook(Book);
    }

    [Test]
    public async Task GetBooksByTitle()
    {
        var response = await HttpService.GetBooksByTitle(Book.Title);
        var listStringBooks = await response.Content.ReadAsStringAsync();
        var json = JsonConvert.DeserializeObject<List<Book>>(listStringBooks);


        Assert.Multiple(() =>
        {
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(json, Is.Not.Empty);
            Assert.That(response, Is.Not.Null);
            Assert.That(json[0].Title, Is.EqualTo(Book.Title));
            Assert.That(json[0].Author, Is.EqualTo(Book.Author));
            Assert.That(json[0].YearOfRelease, Is.EqualTo(Book.YearOfRelease));
        });
    }

    [Test]
    public async Task BookNotFoundByTitle(){

        var book = DataHelper.BookHelper.BookWithTitleAuthorYear("Not Found", "Not Found Author", 1990);
        var response = await HttpService.GetBooksByTitle(book.Title);
        var message = await response.Content.ReadAsStringAsync();
        var s = message.Trim('"');

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(s.Equals(DataHelper.ErrorMessage.NotFoundBook(book)));
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
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response, Is.Not.Null);
            Assert.That(json[0].Title, Is.EqualTo(Book.Title));
            Assert.That(json[0].Author, Is.EqualTo(Book.Author));
            Assert.That(json[0].YearOfRelease, Is.EqualTo(Book.YearOfRelease));
        });
    }
}
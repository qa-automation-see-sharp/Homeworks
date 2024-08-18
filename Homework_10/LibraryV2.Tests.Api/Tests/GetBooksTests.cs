using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Models;
using Newtonsoft.Json;
using System.Net;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    private Book _book {get; set;}
    [OneTimeSetUp]
    public async Task OneTimeSetUp(){
        _book = new()
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = 1980
        };
        var client = _httpService.Configure("http://localhost:5111/");
        await client.CreateDefaultUser();
        await client.Authorize();
        await client.CreateBook(_book);
    }

    [Test]
    public async Task GetBooksByTitle()
    {
        var response = await _httpService.GetBooksByTitle(_book.Title);
        var listStringBooks = await response.Content.ReadAsStringAsync();
        var json = JsonConvert.DeserializeObject<List<Book>>(listStringBooks);


        Assert.Multiple(() =>
        {
            Assert.True(response.IsSuccessStatusCode);
            Assert.IsNotEmpty(json);
            Assert.That(response, Is.Not.Null);
            Assert.That(json[0].Title, Is.EqualTo(_book.Title));
            Assert.That(json[0].Author, Is.EqualTo(_book.Author));
            Assert.That(json[0].YearOfRelease, Is.EqualTo(_book.YearOfRelease));
        });
    }

    [Test]
    public async Task GetBooksByAuthor()
    {
        var response = await _httpService.GetBooksByAuthor(_book.Author);
        var listStringBooks = await response.Content.ReadAsStringAsync();
        var json = JsonConvert.DeserializeObject<List<Book>>(listStringBooks);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(response, Is.Not.Null);
            Assert.That(json[0].Title, Is.EqualTo(_book.Title));
            Assert.That(json[0].Author, Is.EqualTo(_book.Author));
            Assert.That(json[0].YearOfRelease, Is.EqualTo(_book.YearOfRelease));
        });
    }
}
using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Tests.Api.TestHelper;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{

    [Test]
    public async Task GetBooksByTitle_ShouldReturnOk()
    {
        // Arrange
        var book = DataHelper.CreateBook();
        await LibraryHttpService.PostBook(book);

        // Act
        var response = await LibraryHttpService.GetBooksByTitle(book.Title);
        var booksJsonString = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<List<Book>>(booksJsonString);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(books.Count, Is.EqualTo(1));
            Assert.That(books[0].Title, Is.EqualTo(book.Title));
            Assert.That(books[0].Author, Is.EqualTo(book.Author));
            Assert.That(books[0].YearOfRelease, Is.EqualTo(book.YearOfRelease));
        });
    }

    [Test]
    public async Task GetBooksByTitle_BookDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var title = "NotExistingTitle";

        // Act
        var response = await LibraryHttpService.GetBooksByTitle(title);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetBooksByAuthor_ShouldReturnOk()
    {
        // Arrange
        var book = DataHelper.CreateBook();
        await LibraryHttpService.PostBook(book);

        // Act
        var response = await LibraryHttpService.GetBooksByAuthor(book.Author);
        var booksJsonString = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<List<Book>>(booksJsonString);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(books.Count, Is.EqualTo(1));
            Assert.That(books[0].Title, Is.EqualTo(book.Title));
            Assert.That(books[0].Author, Is.EqualTo(book.Author));
            Assert.That(books[0].YearOfRelease, Is.EqualTo(book.YearOfRelease));
        });
    }

    [Test]
    public async Task GetBooksByAuthor_BookDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var author = "NotExistingAuthor";

        // Act
        var response = await LibraryHttpService.GetBooksByAuthor(author);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
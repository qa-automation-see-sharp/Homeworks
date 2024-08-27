using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using Newtonsoft.Json;
using static LibraryV2.Tests.Api.TestHelper.DataHelper;


namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public sealed class CreateBookTests : LibraryV2TestFixture
{
    // WHAT WHEN THEN
    [Test]
    public async Task PostBook_ShouldReturnOk()
    {
        // Arrange
        var book = CreateBook();

        // Act
        var response = await LibraryHttpService.PostBook(book);
        var bookJsonString = await response.Content.ReadAsStringAsync();
        var createdBook = JsonConvert.DeserializeObject<Book>(bookJsonString);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(createdBook.Title, Is.EqualTo(book.Title));
            Assert.That(createdBook.Author, Is.EqualTo(book.Author));
            Assert.That(createdBook.YearOfRelease, Is.EqualTo(book.YearOfRelease));
        });
    }

    [Test]
    public async Task PostBook_AlreadyExist_ShouldReturnBadRequest()
    {
        // Arrange
        var book = CreateBook();

        var response = await LibraryHttpService.PostBook(book);
        var bookJsonString = await response.Content.ReadAsStringAsync();
        var createdBook = JsonConvert.DeserializeObject<Book>(bookJsonString);

        // Act
        var response2 = await LibraryHttpService.PostBook(createdBook);

        // Assert
        Assert.That(response2.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task PostBook_ShouldReturnUnauthorized()
    {
        // Arrange
        var book = CreateBook();

        // Act
        var response = await LibraryHttpService.PostBook(Guid.NewGuid().ToString(), book);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
}
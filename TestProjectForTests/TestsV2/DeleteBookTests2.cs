using System.Net;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.TestHelper;


namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class DeleteBookTests : LibraryV2TestFixture
{
    [Test]
    public async Task DeleteBook_ShouldReturnOk()
    {
        // Arrange
        var book = DataHelper.CreateBook();
        await LibraryHttpService.PostBook(book);

        // Act
        var response = await LibraryHttpService.DeleteBook(book.Title, book.Author);
        var jsonString = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(jsonString, Is.EqualTo($"\"{book.Title} by {book.Author} deleted\""));
        });
    }

    [Test]
    public async Task Delete_NotExistingBook_ShouldReturnNotFound()
    {
        // Arrange
        var book = DataHelper.CreateBook();
        await LibraryHttpService.PostBook(book);

        // Act
        var response1 = await LibraryHttpService.DeleteBook("NotExistingBook", "NotExistingAuthor");

        // Assert
        Assert.That(response1.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
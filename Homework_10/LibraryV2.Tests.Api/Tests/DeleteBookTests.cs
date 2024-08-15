using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;

    [SetUp]
    public new void SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }

    [Test]
    public async Task DeleteBook()
    {
        var newBook = new Book()
        {
            Title = "TitleToDelete",
            Author = "None"
        };

        var bookCreated = await LibraryHttpService.CreateBook(Token, newBook);

        HttpResponseMessage response = await LibraryHttpService.DeleteBook(Token, newBook.Title, newBook.Author);

        var jsonString = await response.Content.ReadAsStringAsync();

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(jsonString.Contains("ToDelete by None deleted"));
        });
        //TODO cover with tests all endpoints from Books controller
        // Delete book
    }
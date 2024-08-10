using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;

    [OneTimeSetUp]
    public void SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }

    [Test]
    public async Task GetBooksByTitle()
    {
        List<Book> response = await _libraryHttpService.GetBooksByTitle("Grasshopper");

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response[0].Title, Is.EqualTo("Grasshopper"));
            Assert.That(response[0].Author, Is.EqualTo("Kotaro Isaka"));
            Assert.That(response[0].YearOfRelease, Is.EqualTo(2004));
        });
    }

    [Test]
    public async Task GetBooksByAuthor()
    {
        List<Book> response = await _libraryHttpService.GetBooksByAuthor("Kotaro Isaka");

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response[0].Title, Is.EqualTo("Grasshopper"));
            Assert.That(response[0].Author, Is.EqualTo("Kotaro Isaka"));
            Assert.That(response[0].YearOfRelease, Is.EqualTo(2004));
        });
    }
}
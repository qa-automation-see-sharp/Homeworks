using LibraryV2.Models;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{
    protected Dictionary<string, string> _bookDetails;
    
    [OneTimeSetUp]
    public async Task SetUp()
    {
        for (var i = 0; i < 3; i++)
        {
            var book = new Book
            {
                Title = "BookTitle" + i,
                Author = "Author" + i,
                YearOfRelease = 199 + i
            };
            await _libraryHttpService.CreateBook(_users.First().Value, book);
            _bookDetails.Add(book.Title, book.Author);
        }
    }

    [OneTimeTearDown]
    public void TearDown()
    {
    }
}
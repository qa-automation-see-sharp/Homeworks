using System.Net;
using LibraryV2.Tests.Api.Fixtures;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    [SetUp]
    public new void SetUp()
    {
    }

    [Test]
    public async Task DeleteBookAsync()
    {
        var response = await _libraryHttpService.DeleteBook(_users.First().Value, _bookDetails.First().Value,
            _bookDetails.First().Key);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
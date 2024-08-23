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
        var token = Users.First().Value.Token;
        var response = await LibraryHttpService.DeleteBook(
            token, 
            _bookDetails.First().Key,
            _bookDetails.First().Value);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
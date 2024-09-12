using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    private readonly LibraryHttpService _httpService = new();

    [OneTimeSetUp]
    public async Task OneTimeSetUpAsync()
    {
    }

    [Test]
    [Description("This test checks if the book is deleted sucessfully")]
    public async Task DeleteBookAsync_WhenBookIsDeleted_ReturnOK()
    {
        var httpResponseMessage = await _libraryHttpService.DeleteBook(_libraryHttpService.GetDefaultUserToken(), _bookDetails.First().Key,
            _bookDetails.First().Value);
        
        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
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
        var client = _httpService.Configure("http://localhost:5111/");
        await client.CreateDefaultUser();
        await client.Authorize();
    }

    [Test]
    public async Task DeleteBookAsync()
    {
        var httpResponseMessage = await _libraryHttpService.DeleteBook(_users.First().Value, _bookDetails.First().Value,
            _bookDetails.First().Key);
        
        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
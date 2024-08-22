using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    private readonly LibraryHttpService _httpService = new();

    [OneTimeSetUp]
    public async Task OneTimeSetUpAsync()
    {
        var client = _httpService.Configure("http://localhost:5111/");
        await client.CreateDefaultUser();
        await client.Authorize();
    }

    [Test, Order(1)]

    public async Task GetBookByTitleAsync()
    {
        var httpResponseMessage = await _httpService.GetBooksByTitle(_bookDetails.First().Key);
        
        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test, Order(2)]

    public async Task GetBookByAuthorAsync()
    {
        var httpResponseMessage = await _httpService.GetBooksByAuthor(_bookDetails.First().Value);
        
        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}

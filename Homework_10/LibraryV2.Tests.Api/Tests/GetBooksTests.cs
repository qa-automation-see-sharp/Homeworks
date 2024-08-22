using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{

    [OneTimeSetUp]
    public async Task OneTimeSetUpAsync()
    {
        
    }

    [Test, Order(1)]

    public async Task GetBookByTitleAsync()
    {
        var httpResponseMessage = await _libraryHttpService.GetBooksByTitle(_bookDetails.First().Key);
        
        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test, Order(2)]

    public async Task GetBookByAuthorAsync()
    {
        var httpResponseMessage = await _libraryHttpService.GetBooksByAuthor(_bookDetails.First().Value);
        
        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}

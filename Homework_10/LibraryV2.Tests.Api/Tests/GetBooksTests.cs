using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;

    [SetUp]
    public void SetUp()
    {
    }

    [Test, Order(1)]

    public async Task GetBookByTitleAsync()
    {
        var response = await _libraryHttpService.GetBooksByTitle(_bookDetails.First().Key);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test, Order(2)]

    public async Task GetBookByAuthorAsync()
    {
        var response = await _libraryHttpService.GetBooksByAuthor(_bookDetails.First().Value);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}

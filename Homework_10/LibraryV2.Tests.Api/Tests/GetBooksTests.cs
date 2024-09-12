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
    [Description("This test checks if the book is found by its title sucessfully")]

    public async Task GetBookByTitleAsync_ReturnOK()
    {
        var httpResponseMessage = await _libraryHttpService.GetBooksByTitle(_bookDetails.First().Key);
        
        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test, Order(2)]
    [Description("This test checks if the book is found by its author sucessfully")]

    public async Task GetBookByAuthorAsync_ReturnOK()
    {
        var httpResponseMessage = await _libraryHttpService.GetBooksByAuthor(_bookDetails.First().Value);
        
        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}

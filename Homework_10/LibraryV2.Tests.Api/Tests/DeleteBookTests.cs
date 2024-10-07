using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.TestHelpers;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    [OneTimeSetUp]
    public async Task OneTimeSetUpAsync()
    {
        await _libraryHttpService.LogIn(_libraryHttpService.DefaultUser, true);
    }

    [Test]
    [Description("This test checks if the book is deleted successfully")]
    public async Task DeleteBookAsync_ReturnOK()
    {
        var book = DataHelper.CreateBook();
            
        var httpResponseMessage = 
            await _libraryHttpService.PostBook(_libraryHttpService.DefaultUserAuthToken.Token, book);
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var bookFromResponse = JsonConvert.DeserializeObject<Book>(content);
        
        var deleteResponseMessage = 
            await _libraryHttpService
                .DeleteBook(_libraryHttpService.DefaultUserAuthToken.Token, bookFromResponse.Title,
                    bookFromResponse.Author);
        
        Assert.That(deleteResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
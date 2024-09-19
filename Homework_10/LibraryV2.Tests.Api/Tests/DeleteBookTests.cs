using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;

    [SetUp]
    public new void SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }

    //TODO cover with tests all endpoints from Books controller
    // Delete book
    [Test]

    public async Task DeleteBook()
    {
        var usertocreate = GenerateTestUser();
        HttpResponseMessage responceCreateUser = await _libraryHttpService.CreateUser(usertocreate);
        HttpResponseMessage responceLoginUser = await _libraryHttpService.LogIn(usertocreate);
        var jsonString = await responceLoginUser.Content.ReadAsStringAsync();
        var userToAssert = JsonConvert.DeserializeObject<AuthorizationToken>(jsonString);

        var booktocreate = GenerateTestBook();
        HttpResponseMessage responceCreateBook = await _libraryHttpService.CreateBook(userToAssert.Token, booktocreate);
        HttpResponseMessage responceDeleteBook = await _libraryHttpService.DeleteBook(userToAssert.Token, booktocreate.Title, booktocreate.Author);
        

        Assert.Multiple(() =>
        {
            Assert.That(responceDeleteBook.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            
        });
    }
    private User GenerateTestUser()
    {
        return new User
        {
            FullName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            NickName = Guid.NewGuid().ToString()
        };
    }
    private Book GenerateTestBook()
    {
        return new Book
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = new Random().Next(1850, 2024)
        };
    }
}
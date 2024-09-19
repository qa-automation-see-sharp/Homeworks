
namespace LibraryV2.Tests.Api.Tests;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

public class CreateBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;
    
    [SetUp]
    public new void SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }

    //TODO cover with tests all endpoints from Books controller
    // Create book
    [Test]
    public async Task CreateBook()
    {
        var usertocreate = GenerateTestUser();
        HttpResponseMessage responceCreateUser = await _libraryHttpService.CreateUser(usertocreate);
        HttpResponseMessage responceLoginUser = await _libraryHttpService.LogIn(usertocreate);
        var jsonString = await responceLoginUser.Content.ReadAsStringAsync();
        var userToAssert = JsonConvert.DeserializeObject<AuthorizationToken>(jsonString);
        
        
        var booktocreate = GenerateTestBook();
        HttpResponseMessage responceCreateBook = await _libraryHttpService.CreateBook(userToAssert.Token, booktocreate);
        var jsonString2 = await responceCreateBook.Content.ReadAsStringAsync();
        var bookToAssert = JsonConvert.DeserializeObject<Book>(jsonString2);

        Assert.Multiple(() =>
        {
            Assert.That(responceCreateBook.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(bookToAssert.Title, Is.EqualTo(booktocreate.Title));
            Assert.That(bookToAssert.Author, Is.EqualTo(booktocreate.Author));
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

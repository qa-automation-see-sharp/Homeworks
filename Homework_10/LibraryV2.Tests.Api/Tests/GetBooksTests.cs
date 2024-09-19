using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;
using LibraryV2.Models;
using System.Net;
using Newtonsoft.Json.Linq;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;

    [SetUp]
    public void SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }
    [Test]

    public async Task GetBookbyTitle()
    {
        var usertocreate = GenerateTestUser();
        HttpResponseMessage responcecreateuser = await _libraryHttpService.CreateUser(usertocreate);
        HttpResponseMessage responceloginuser = await _libraryHttpService.LogIn(usertocreate);
        var jsonStringLogin = await responceloginuser.Content.ReadAsStringAsync();
        var userToAssert = JsonConvert.DeserializeObject<AuthorizationToken>(jsonStringLogin);

        var booktocreate = GenerateTestBook();
        HttpResponseMessage responceCreateBook = await _libraryHttpService.CreateBook(userToAssert.Token, booktocreate);
        HttpResponseMessage responceGetBook = await _libraryHttpService.GetBooksByTitle(booktocreate.Title);
        var jsonStringGetBook = await responceGetBook.Content.ReadAsStringAsync();
        var bookToAssert = JsonConvert.DeserializeObject<List<Book>>(jsonStringGetBook);

        Assert.Multiple(() =>
        {
            Assert.That(responceGetBook.StatusCode, Is.EqualTo(HttpStatusCode.OK)); 
        }
       );
    }
    [Test]

    public async Task GetBookbyAuthor()
    {
        var usertocreate = GenerateTestUser();
        HttpResponseMessage responceCreateUser = await _libraryHttpService.CreateUser(usertocreate);
        HttpResponseMessage responceLoginUser = await _libraryHttpService.LogIn(usertocreate);
        var jsonStringLogin = await responceLoginUser.Content.ReadAsStringAsync();
        var userToAssert = JsonConvert.DeserializeObject<AuthorizationToken>(jsonStringLogin);

        var booktocreate = GenerateTestBook();
        HttpResponseMessage responceCreateBook = await _libraryHttpService.CreateBook(userToAssert.Token, booktocreate);
        HttpResponseMessage responceGetBook = await _libraryHttpService.GetBooksByAuthor(booktocreate.Author);
        var jsonStringGetBook = await responceGetBook.Content.ReadAsStringAsync();
        var bookToAssert = JsonConvert.DeserializeObject<List<Book>>(jsonStringGetBook);

        Assert.Multiple(() =>
        {
            Assert.That(responceGetBook.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
       );
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
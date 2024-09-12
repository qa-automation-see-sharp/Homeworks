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
        var usertocreate = new User
        {
            FullName = "SergiiPavliuchyk",
            Password = "password4",
            NickName = "SergiiPavliuchyk"
        };
        HttpResponseMessage responcecreateuser = await _libraryHttpService.CreateUser(usertocreate);
        HttpResponseMessage responceloginuser = await _libraryHttpService.LogIn(usertocreate);
        var jsonStringLogin = await responceloginuser.Content.ReadAsStringAsync();
        var userToAssert = JsonConvert.DeserializeObject<User>(jsonStringLogin);
        JObject json = JObject.Parse(jsonStringLogin);
        string token = json["token"].ToString();

        var booktocreate = new Book
        {
            Title = "Kobzar",
            Author = "Shevchenko",
            YearOfRelease = new Random().Next(1850, 2024)
        };
        HttpResponseMessage responceCreateBook = await _libraryHttpService.CreateBook(token, booktocreate);
        HttpResponseMessage responceGetBook = await _libraryHttpService.GetBooksByTitle(booktocreate.Title);
        var jsonStringGetBook = await responceGetBook.Content.ReadAsStringAsync();
        var bookToAssert = JsonConvert.DeserializeObject<Book>(jsonStringGetBook);

        Assert.Multiple(() =>
        {
            Assert.That(responceGetBook.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            
        }
       );
    }
    [Test]

    public async Task GetBookbyAuthor()
    {
        var usertocreate = new User
        {
            FullName = "NinaPavliuchyk",
            Password = "password5",
            NickName = "NinaPavliuchyk"
        };
        HttpResponseMessage responce1 = await _libraryHttpService.CreateUser(usertocreate);
        HttpResponseMessage responce = await _libraryHttpService.LogIn(usertocreate);
        var jsonString = await responce.Content.ReadAsStringAsync();
        var userToAssert = JsonConvert.DeserializeObject<User>(jsonString);
        JObject json = JObject.Parse(jsonString);
        string token = json["token"].ToString();

        var booktocreate = new Book
        {
            Title = "Biblia",
            Author = "God",
            YearOfRelease = new Random().Next(1850, 2024)
        };
        HttpResponseMessage responce2 = await _libraryHttpService.CreateBook(token, booktocreate);
        HttpResponseMessage responceGetBook = await _libraryHttpService.GetBooksByAuthor(booktocreate.Author);
        var jsonStringGetBook = await responceGetBook.Content.ReadAsStringAsync();
        var bookToAssert = JsonConvert.DeserializeObject<Book>(jsonStringGetBook);

        Assert.Multiple(() =>
        {
            Assert.That(responceGetBook.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            
        }
       );
    }
}
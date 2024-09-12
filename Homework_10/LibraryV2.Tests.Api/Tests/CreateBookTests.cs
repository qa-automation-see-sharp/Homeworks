
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
        var usertocreate = new User
        {
            FullName = "OksanaPavliuchyk",
            Password = "password2",
            NickName = "OksanaPavliuchyk"
        };
        HttpResponseMessage responce1 = await _libraryHttpService.CreateUser(usertocreate);
        HttpResponseMessage responce = await _libraryHttpService.LogIn(usertocreate);
        var jsonString = await responce.Content.ReadAsStringAsync();
        var userToAssert = JsonConvert.DeserializeObject<User>(jsonString);
        JObject json = JObject.Parse(jsonString);
        string token = json["token"].ToString();
        Console.WriteLine(token);

        var booktocreate = new Book
        {
            Title = "Bukvar",
            Author = "Narod",
            YearOfRelease = new Random().Next(1850, 2024)
        };
        HttpResponseMessage responce2 = await _libraryHttpService.CreateBook(token, booktocreate);
        var jsonString2 = await responce2.Content.ReadAsStringAsync();
        var bookToAssert = JsonConvert.DeserializeObject<Book>(jsonString2);

        Assert.Multiple(() =>
        {
            Assert.That(responce2.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(bookToAssert.Title, Is.EqualTo(booktocreate.Title));
            Assert.That(bookToAssert.Author, Is.EqualTo(booktocreate.Author));
        });
    }
}

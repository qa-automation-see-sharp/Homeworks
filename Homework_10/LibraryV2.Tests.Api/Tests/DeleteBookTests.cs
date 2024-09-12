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
        var usertocreate = new User
        {
            FullName = "OlgaPavliuchyk",
            Password = "password3",
            NickName = "OlgaPavliuchyk"
        };
        HttpResponseMessage responce1 = await _libraryHttpService.CreateUser(usertocreate);
        HttpResponseMessage responce = await _libraryHttpService.LogIn(usertocreate);
        var jsonString = await responce.Content.ReadAsStringAsync();
        var userToAssert = JsonConvert.DeserializeObject<User>(jsonString);
        JObject json = JObject.Parse(jsonString);
        string token = json["token"].ToString();

        var booktocreate = new Book
        {
            Title = "TiniPredkiv",
            Author = "MykhailoKotsyubynskyi",
            YearOfRelease = new Random().Next(1850, 2024)
        };
        HttpResponseMessage responce2 = await _libraryHttpService.CreateBook(token, booktocreate);
        HttpResponseMessage responce4 = await _libraryHttpService.DeleteBook(token, booktocreate.Title, booktocreate.Author);
        

        Assert.Multiple(() =>
        {
            Assert.That(responce4.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            
        });
    }
}
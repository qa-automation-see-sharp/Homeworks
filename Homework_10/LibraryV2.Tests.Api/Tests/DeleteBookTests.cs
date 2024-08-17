using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Tests;

public class DeleteBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;

    [SetUp]
    public void Setup()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }

    [Test]
    public async Task DeleteBook200()
    {
        var user = new User
        {
            FullName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            NickName = Guid.NewGuid().ToString()
        };

        await _libraryHttpService.CreateUser(user);
        var httpResponseMessageUser = await _libraryHttpService.LogIn(user);
        var contentUser = await httpResponseMessageUser.Content.ReadAsStringAsync();
        var responseUser = JsonConvert.DeserializeObject<AuthorizationToken>(contentUser);

        var book = new Book
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = 0000
        };

        await _libraryHttpService.CreateBook(responseUser.Token, book);
        var httpResponseMessage = await _libraryHttpService.DeleteBook(responseUser.Token, book.Title, book.Author);

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task DeleteBook404()
    {
        var user = new User
        {
            FullName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            NickName = Guid.NewGuid().ToString()
        };

        await _libraryHttpService.CreateUser(user);
        var httpResponseMessageUser = await _libraryHttpService.LogIn(user);
        var contentUser = await httpResponseMessageUser.Content.ReadAsStringAsync();
        var responseUser = JsonConvert.DeserializeObject<AuthorizationToken>(contentUser);

        var httpResponseMessage = await _libraryHttpService.DeleteBook(responseUser.Token, "book", "author");

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task DeleteBook401()
    {
        var httpResponseMessage = await _libraryHttpService.DeleteBook("token", "book", "author");

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
}
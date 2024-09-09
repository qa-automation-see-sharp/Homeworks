using System;
using System.Net;
using System.Text;
using LibraryV2.Models;
using LibraryV2.Services;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class CreateBookTests : LibraryV2TestFixture
{
    private readonly LibraryHttpService _libraryHttpService = new();

    private Book _testBook;
    private string _token;
    private string _time = DateTime.Now.ToString("yyyyMMddHHmmss");

    [OneTimeSetUp]
    public async Task SetUp()
    {
        var client = _libraryHttpService.Configure("http://localhost:5111/");
        await client.CreateTestUser();
        await client.LogInTestUser();

        //create new instance of Book and User
        _testBook = new Book
        {
            Title = "The Book",
            Author = "The Author",
            YearOfRelease = 2021
        };

        HttpResponseMessage response = await _libraryHttpService.CreateBook(_testBook);
        var jsonString = await response.Content.ReadAsStringAsync();
        var bookResponse = JsonConvert.DeserializeObject<Book>(jsonString);
    }

    [Test]
    public async Task CreateBook_WithNoAuthorizedUser_ReturnsUnauthorized()
    {
        //create new instance of Book
        var book = new Book
        {
            Title = "New Book",
            Author = "New Author",
            YearOfRelease = 2021
        };

        //request to create book without authorization
        var response = await _libraryHttpService.CreateBook(_token, book);

        // Asserts
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.NotNull(response);
            _logger.LogInformation("Token used for creating book: {Token}", _token);
        });
    }

    [Test]
    public async Task CreateNewBook_ReturnCreated()
    {
        var book = new Book
        {
            Title = "Title" + _time,
            Author = "Author" + _time,
            YearOfRelease = new Random().Next(1800, 2024)
        };

        HttpResponseMessage response = await _libraryHttpService.CreateBook(book);
        var jsonString = await response.Content.ReadAsStringAsync();
        var bookResponse = JsonConvert.DeserializeObject<Book>(jsonString);
        

        //Asserts
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response);
            Assert.AreEqual(book.Title, bookResponse.Title);
            Assert.AreEqual(book.Author, bookResponse.Author);
            Assert.AreEqual(book.YearOfRelease, bookResponse.YearOfRelease);
        });

        await _libraryHttpService.DeleteBook(book.Title, book.Author);
    }

    [Test]
    public async Task CreateBook_WithSameName_ReturnsBadRequest()
    {
        

        //create instance book with the same title and author
        var book = new Book
        {
           Title = "The Book",
            Author = "The Author",
            YearOfRelease = 2021
        };

        //send request to create new book
        HttpResponseMessage response = await _libraryHttpService.CreateBook(book);
        var jsonString = await response.Content.ReadAsStringAsync();

        //Asserts
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(response);
        });
    }
    [OneTimeTearDown]
    public async Task TearDown()
    {
        if (_testBook != null)
        {
            await _libraryHttpService.DeleteBook(_testBook.Title, _testBook.Author);
        }

        _testBook = null;
    }
    
}
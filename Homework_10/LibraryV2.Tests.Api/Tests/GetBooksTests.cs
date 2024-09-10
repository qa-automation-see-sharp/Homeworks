using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;
using LibraryV2.Endpoints.Books;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity.Data;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class GetBooksTests : LibraryV2TestFixture
{
    private readonly LibraryHttpService _libraryHttpService = new();
    private string _token;
    private Book _testBooks;
    private List<List<Book>> _library;
    Random year = new Random();

    [OneTimeSetUp]
    public new async Task SetUp()
    {
        var client = _libraryHttpService.Configure("http://localhost:5111/");
        await client.CreateTestUser();
        await client.LogInTestUser();

        //create library
        _library = new()
        {
            new()
            {
                new Book() { Title = "The Book 1", Author = "The Author 1", YearOfRelease = year.Next(0001, 2024) },
                new Book() { Title = "The Book 2", Author = "The Author 1", YearOfRelease = year.Next(0001, 2024) },
                new Book() { Title = "The Book 3", Author = "The Author 1", YearOfRelease = year.Next(0001, 2024) }
            },
            new()
            {
                new Book() { Title = "The Book 1", Author = "The Author 2", YearOfRelease = year.Next(0001, 2024) },
                new Book() { Title = "The Book 2", Author = "The Author 2", YearOfRelease = year.Next(0001, 2024) },
                new Book() { Title = "The Book 3", Author = "The Author 2", YearOfRelease = year.Next(0001, 2024) }
            },
            new()
            {
                new() { Title = "The Book 1", Author = "The Author 3", YearOfRelease = year.Next(0001, 2024) },
                new() { Title = "The Book 2", Author = "The Author 3", YearOfRelease = year.Next(0001, 2024) },
                new() { Title = "The Book 3", Author = "The Author 3", YearOfRelease = year.Next(0001, 2024) }
            }
        };

        //post books to library
        foreach (var books in _library)
        {
            foreach (var book in books)
            {
                await _libraryHttpService.CreateBook(book);
            }
        }
    }

    [Test]
    public async Task GetBookByTitle_IfBookExist_ReturnOK()
    {
        var testTitle = "The Book 1";
        HttpResponseMessage response = await _libraryHttpService.GetBooksByTitle(testTitle);
        var json = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<List<Book>>(json);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);
            Assert.IsTrue(books.All(book => book.Title == testTitle));
        });
    }

    [Test]
    public async Task GetBookByTitle_IfBookDoesntExist_ReturnNotFound()
    {
        var testTitle = "The Book 7";
        HttpResponseMessage response = await _libraryHttpService.GetBooksByAuthor(testTitle);
        var json = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(json);
        });
    }

    [Test]
    public async Task GetBooksByAuthor_IfAuthorExist_ReturnOK()
    {
        var testAuthor = "The Author 1";
        HttpResponseMessage response = await _libraryHttpService.GetBooksByAuthor(testAuthor);
        var json = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<List<Book>>(json);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);
            Assert.IsTrue(books.All(book => book.Author == testAuthor));
        });
    }

    [Test]
    public async Task GetBooksByAuthor_IfAuthorDoesntExist_ReturnNotFound()
    {
        var testAuthor = "The Author 7";
        HttpResponseMessage response = await _libraryHttpService.GetBooksByTitle(testAuthor);
        var json = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(json);
        });
    }
    //PRINT ALL BOOKS
    [Test]
    public async Task GetAllBooks()
    {
        //send request to get all books
        HttpResponseMessage response = await _libraryHttpService.GetAllBooks();
        var json = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<List<Book>>(json);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);
            Assert.AreEqual(9, books.Count);
        });
    }
}
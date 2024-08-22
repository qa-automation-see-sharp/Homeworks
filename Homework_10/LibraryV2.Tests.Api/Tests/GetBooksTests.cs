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
    //create new private variables
    private LibraryHttpService _libraryHttpService;
    private string _token;
    private Book _testBooks;
    private List<List<Book>> _library;
    private User _testUser;
    Random year = new Random();

    [SetUp]
    public async Task SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");

        //create test user
        _testUser = new User
        {
            FullName = "123",
            NickName = "qwerty",
            Password = "asdf"
        };

        //creat new user and get token
        var createTestUserResponse = await _libraryHttpService.CreateUser(_testUser);
        TestContext.WriteLine($"Create User Status Code: {createTestUserResponse.StatusCode}");
        var loginResponse = await _libraryHttpService.LogIn(_testUser);
        TestContext.WriteLine($"Login Status Code: {loginResponse.StatusCode}");
        _token = (await loginResponse.Content.ReadAsStringAsync()).Trim('"'); //remove quotes from token
        TestContext.WriteLine($"Token: {_token}");

        //create library
        _library = new List<List<Book>>
        {
            new List<Book>
            {
                new Book  {Title = "The Book 1", Author = "The Author 1", YearOfRelease = year.Next(0001, 2024) },
                new Book  {Title = "The Book 2", Author = "The Author 1", YearOfRelease = year.Next(0001, 2024) },
                new Book  {Title = "The Book 3", Author = "The Author 1", YearOfRelease = year.Next(0001, 2024) }
            },
            new List<Book>
            {
                new Book  {Title = "The Book 1", Author = "The Author 2", YearOfRelease = year.Next(0001, 2024) },
                new Book  {Title = "The Book 2", Author = "The Author 2", YearOfRelease = year.Next(0001, 2024) },
                new Book  {Title = "The Book 3", Author = "The Author 2", YearOfRelease = year.Next(0001, 2024) }
            },
            new List<Book>
            {
                new Book  {Title = "The Book 1", Author = "The Author 3", YearOfRelease = year.Next(0001, 2024) },
                new Book  {Title = "The Book 2", Author = "The Author 3", YearOfRelease = year.Next(0001, 2024) },
                new Book  {Title = "The Book 3", Author = "The Author 3", YearOfRelease = year.Next(0001, 2024) }
            }
        };


        //post books to library

        foreach (var books in _library)
        {
            foreach (var book in books)
            {
                var response = await _libraryHttpService.CreateBook(_token, book);
            }
        }
    }

    //PRINT ALL BOOK BY TITLES
    [Test]
    [TestCase("The Book 1")]
    [TestCase("The Book 7")]
    public async Task GetBookByTitle(string bookTitle)
    {
        //create test case
        string newtitle = bookTitle;

        //send request to get books by title
        var response = await _libraryHttpService.GetBooksByTitle(newtitle);
        TestContext.WriteLine($"Get Books By Title Status Code: {response.StatusCode}");

        //Assert
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            var notFoundResponse = await response.Content.ReadFromJsonAsync<string>();
            TestContext.WriteLine($"Not Found Response: {notFoundResponse}");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
            Assert.NotNull(notFoundResponse);
            TestContext.WriteLine($"Response Status Code: {response.StatusCode}");
        }
        else
        {
            var books = await response.Content.ReadFromJsonAsync<List<Book>>();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);

            //print books by title
            foreach (var book in books)
            {
                if (book.Title == newtitle)
                {
                    TestContext.WriteLine($"Book Title: {book.Title}");
                }
            }
        }
    }

    //PRINT ALL BOOKS BY AUTHORS
    [Test]
    [TestCase("The Author 1")]
    [TestCase("The Author 7")]
    public async Task GetBooksByAuthor(string authorName)
    {
        //create test case
        string author = authorName;

        //send request to get books by author
        var response = await _libraryHttpService.GetBooksByAuthor(author);
        TestContext.WriteLine($"Get Books By Author Status Code: {response.StatusCode}");

        //Assert
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            var notFoundResponse = await response.Content.ReadFromJsonAsync<string>();
            TestContext.WriteLine($"Not Found Response: {notFoundResponse}");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
            Assert.NotNull(notFoundResponse);
            TestContext.WriteLine($"Response Status Code: {response.StatusCode}");
        }
        else
        {
            var books = await response.Content.ReadFromJsonAsync<List<Book>>();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);

            //print books by author
            foreach (var book in books)
            {
                if (book.Author == author)
                {
                    TestContext.WriteLine($"Book Author: {book.Author}");
                }
            }
        }
    }

    //PRINT ALL BOOKS
    [Test]
    public async Task GetAllBooks()
    {
        //send request to get all books
        var response = await _libraryHttpService.GetAllBooks();
        TestContext.WriteLine($"Get All Books Status Code: {response.StatusCode}");

        //Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response);
        var books = await response.Content.ReadFromJsonAsync<List<Book>>();
        Assert.NotNull(books);

        //print all books
        foreach (var book in books)
        {
            TestContext.WriteLine($"Book Title: {book.Title}, Author: {book.Author}, Year of Release: {book.YearOfRelease}");
        }
    }
   
}


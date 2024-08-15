using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity.Data;
using LibraryV2.Services;
using System.Runtime.CompilerServices;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class CreateBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;
    private string _token;
    private string _time = DateTime.Now.ToString("yyyyMMddHHmmss");
    private Book _testBook;
    private User _testUser;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        //create new instance of LibraryHttpService
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");

        //create new instance of Book and User
        _testBook = new Book
        {
            Title = "The Book",
            Author = "The Author",
            YearOfRelease = 2021
        };

        _testUser = new User
        {
            FullName = "123",
            NickName = "qwerty",
            Password = "asdf"
        };

        //create new user
        var createTestUserResponse = await _libraryHttpService.CreateUser(_testUser);
        TestContext.WriteLine($"Create User Status Code: {createTestUserResponse.StatusCode}");

    }

    //TODO cover with tests all endpoints from Books controller

    // CREATE BOOK WITHOUT AUTHORIZATION
    [Test]
    public async Task CreateBookNoAuth()
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
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.NotNull(response);
        TestContext.WriteLine($"Created Book Status Code: {response.StatusCode}");
        TestContext.WriteLine($"Token: {_token}");
    }

    //CREATE NEW BOOK WITH AUTHORIZATION 
    [Test]
    
    public async Task CreateNewBook ()

    {
        //login user and get token
        var loginUserResponse = await _libraryHttpService.LogIn(_testUser);
        _token = (await loginUserResponse.Content.ReadAsStringAsync()).Trim('"'); //remove quotes from token

        //send request to create new book
        var postTestBook = await _libraryHttpService.CreateBook(_token, _testBook);

        //create instance book with new title and new author
        var book = new Book
        {
            Title = "Title" + _time,
            Author = "Author" + _time,
            YearOfRelease = new Random().Next(1800, 2024)
        };

        //send request to create new book
        var bookCreateNewResponse = await _libraryHttpService.CreateBook(_token, book);
        TestContext.WriteLine(bookCreateNewResponse.StatusCode);

        //Asserts
        Assert.AreEqual(HttpStatusCode.Created, bookCreateNewResponse.StatusCode);
        Assert.NotNull(bookCreateNewResponse);
        Assert.AreNotEqual(book.Title, _testBook.Title);
        TestContext.WriteLine($"New book title: ==> {book.Title} <== exist book title: ==> {_testBook.Title} <==");
        Assert.AreNotEqual(book.Author, _testBook.Author);
        TestContext.WriteLine($"New book author: ==> {book.Author} <== exist book author: ==> {_testBook.Title} <==");
    }

    //CREATE NEW BOOK WITH DUPLICATE TITLE
    [Test]

    public async Task CreateDupBook()

    {
        //login user and get token
        var loginUserResponse = await _libraryHttpService.LogIn(_testUser);
        _token = (await loginUserResponse.Content.ReadAsStringAsync()).Trim('"');//remove quotes from token

        //send request to create new book
        var postTestBook = await _libraryHttpService.CreateBook(_token, _testBook);

        //create instance book with the same title and author
        var book = new Book
        {
            Title = "The Book",
            Author = "The Author",
            YearOfRelease = 2021
        };

        //send request to create new book
        var bookCreateNewResponse = await _libraryHttpService.CreateBook(_token, book);
        TestContext.WriteLine(bookCreateNewResponse.StatusCode);

        //asserts
        Assert.AreEqual(HttpStatusCode.BadRequest, bookCreateNewResponse.StatusCode);
        Assert.NotNull(bookCreateNewResponse);
        Assert.AreEqual(book.Title, _testBook.Title);
        TestContext.WriteLine($"New book title: ==> {book.Title} <== exist book title: ==> {_testBook.Title} <==");
        Assert.AreEqual(book.Author, _testBook.Author);
        TestContext.WriteLine($"New book author: ==> {book.Author} <== exist book author: ==> {_testBook.Title} <==");
    }

    [TestCase("The Book", "The Author", 2021)]
    [TestCase("The Book New", "The Author", 2084)]
    [TestCase("The Book", "The Author New", 1763)]
    [TestCase("The Book New", "The Author New", 1234)]

    public async Task CreateBook (string title, string author, int yearOfRelease)

    {
        //create new instance of Book
        var book = new Book
        {
            Title = title,
            Author = author,
            YearOfRelease = yearOfRelease
        };

        //login user and get token
        var loginUserResponse = await _libraryHttpService.LogIn(_testUser);
        TestContext.WriteLine($"Login Response: {loginUserResponse.StatusCode}");
        _token = (await loginUserResponse.Content.ReadAsStringAsync()).Trim('"');//remove quotes from token
        
        //send request to create test book
        var postTestBook = await _libraryHttpService.CreateBook(_token, _testBook);

        //send request to create new book
        var bookCreateNewResponse = await _libraryHttpService.CreateBook(_token, book);
        TestContext.WriteLine($"Create Book Response: {bookCreateNewResponse.StatusCode}");

        if (title == "The Book" && author == "The Author")
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, bookCreateNewResponse.StatusCode);
            Assert.NotNull(bookCreateNewResponse);
            var answerContent = await bookCreateNewResponse.Content.ReadAsStringAsync();
            TestContext.WriteLine($"Created new book response status: {answerContent}");
            TestContext.WriteLine($"Expected title: {title} \nactual title: {book.Title}");
            TestContext.WriteLine($"Expected author: {author} \nactual author: {book.Author}");
            TestContext.WriteLine($"Expected year of release: {yearOfRelease} \nactual year of release: {book.YearOfRelease}");
        }
        else
        {
            Assert.AreEqual(HttpStatusCode.Created, bookCreateNewResponse.StatusCode);
            Assert.NotNull(bookCreateNewResponse);
            var answerContent = await bookCreateNewResponse.Content.ReadAsStringAsync();
            TestContext.WriteLine($"{answerContent}");
        }
    }

}
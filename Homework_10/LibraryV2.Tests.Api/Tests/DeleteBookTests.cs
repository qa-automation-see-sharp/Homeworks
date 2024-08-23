using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

[TestFixture]
public class DeleteBookTests : LibraryV2TestFixture
{
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
        foreach (var book in books)
        {
            var response = await _libraryHttpService.CreateBook(_token, book);
        }
    }

    //create new private variables
    private LibraryHttpService _libraryHttpService;
    private string _token;
    private Book _testBooks;
    private List<List<Book>> _library;
    private User _testUser;
    private readonly Random year = new();


    //TODO cover with tests all endpoints from Books controller
    //DELETE BOOK
    [Test]
    [TestCase("0000", "The Book 1", "The Author 1")]
    [TestCase("0000", "The Book 2", "The Author 7")]
    public async Task deleteBook(string tokenParam, string titleParam, string authorParam)
    {
        var book = new Book
        {
            Title = titleParam,
            Author = authorParam
        };

        //request to delete book
        var deleteResponse = await _libraryHttpService.DeleteBook(_token, book.Title, book.Author);
        if (deleteResponse.StatusCode == HttpStatusCode.OK)
        {
            var jsonRespond = await deleteResponse.Content.ReadAsStringAsync();
            TestContext.WriteLine($"Delete Book Response: {jsonRespond}");
            Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.NotNull(deleteResponse);
            TestContext.WriteLine($"Deleted Book Status Code: {deleteResponse.StatusCode}");
        }
        else if (deleteResponse.StatusCode == HttpStatusCode.NotFound)
        {
            var jsonRespond = await deleteResponse.Content.ReadAsStringAsync();
            TestContext.WriteLine($"Delete Book Response: {jsonRespond}");
            Assert.AreEqual(HttpStatusCode.NotFound, deleteResponse.StatusCode);
            Assert.NotNull(deleteResponse);
            TestContext.WriteLine($"Deleted Book Status Code: {deleteResponse.StatusCode}");
        }
    }

    //DELETE BOOK WITHOUT AUTHORIZATION
    [Test]
    public async Task deleteBookNoAuth()
    {
        //create new instance of Book
        var book = new Book
        {
            Title = "New Book",
            Author = "New Author",
            YearOfRelease = 2021
        };
        _token = null;
        //request to delete book without authorization
        var response = await _libraryHttpService.DeleteBook(_token, book.Title, book.Author);

        // Asserts
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.NotNull(response);
        TestContext.WriteLine($"Deleted Book Status Code: {response.StatusCode}");
        TestContext.WriteLine($"Token: {_token}");
    }

    //DELETE BOOK NOT FOUND
    [Test]
    public async Task deleteBookNotFound()
    {
        //create new instance of Book
        var book = new Book
        {
            Title = "New Book",
            Author = "New Author",
            YearOfRelease = 2021
        };

        //request to delete book without authorization
        var response = await _libraryHttpService.DeleteBook(_token, book.Title, book.Author);

        // Asserts
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        Assert.NotNull(response);
        TestContext.WriteLine($"Deleted Book Status Code: {response.StatusCode}");
        TestContext.WriteLine($"Token: {_token}");
    }

    //DELETE BOOK BY TITLE
    [Test]
    public async Task deleteBookByTitle()
    {
        var titleToDel = "The Book 1";
        var authorToDel = "The Author 1";


        var createTest = await _libraryHttpService.CreateBook(_token,
            new Book { Title = titleToDel, Author = authorToDel, YearOfRelease = 2021 });
        TestContext.WriteLine($"Create Book Status Code: {createTest.StatusCode}");

        var someTest = await _libraryHttpService.GetAllBooks();
        TestContext.WriteLine($"Get All Books Status Code: {someTest.StatusCode}");

        var deleteBookResponse = await _libraryHttpService.DeleteBook(_token, titleToDel, authorToDel);
        TestContext.WriteLine($"Delete Book Status Code: {deleteBookResponse.StatusCode}");
        var jsonRespond = await deleteBookResponse.Content.ReadAsStringAsync();
        TestContext.WriteLine($"Delete Book Response: {jsonRespond}");
    }
}
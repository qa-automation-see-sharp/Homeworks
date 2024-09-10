using System.Net;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Tests;

//TODO For method names use PascalCase  (deleteBook_IfExist_ReturnOK -> Delete_ExistingBook_ReturnOK) 
[TestFixture]
public class DeleteBookTests : LibraryV2TestFixture
{
    //create variables
    private string _token;
    private Book _testBooks;
    private List<List<Book>> _library;
    Random year = new Random();
    private string _time = DateTime.Now.ToString("yyyyMMddHHmmss");

    //create instance of LibraryHttpService
    private readonly LibraryHttpService _libraryHttpService = new();

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        //configure client
        var client = _libraryHttpService.Configure("http://localhost:5111/");
        await client.CreateTestUser();
        await client.LogInTestUser();
    }

    [SetUp]  
    public async Task SetUp()
    {
        //create library
        _library = new List<List<Book>>
        {
            new ()
            {
                new Book() { Title = "The Book 1", Author = "The Author 1", YearOfRelease = year.Next(0001, 2024) },
                new Book() { Title = "The Book 2", Author = "The Author 1", YearOfRelease = year.Next(0001, 2024) },
                new Book() { Title = "The Book 3", Author = "The Author 1", YearOfRelease = year.Next(0001, 2024) }
},
            new ()
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
                var response = await _libraryHttpService.CreateBook(book);
            }
        }
}

    [Test]
    public async Task deleteBook_IfExist_ReturnOK()
    {
        //get first book from library
        var book = _library.SelectMany(List => List).FirstOrDefault();
        //request to delete book
        HttpResponseMessage response = await _libraryHttpService.DeleteBook(book.Title, book.Author);
        //get response as string
        var json = await response.Content.ReadAsStringAsync();

        //Asserts
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            TestContext.WriteLine($"Deleted Book Status Code: {response.StatusCode}");
        });
    }

    [Test]
    public async Task deleteBook_IfDoesntExist_returnNotFound()
    {
        //create new book that doesnt exist in library
        var book = new Book
        {
            Title = "The Book" + _time,
            Author = "The Author" + _time
        };

        //request to delete book
        HttpResponseMessage response = await _libraryHttpService.DeleteBook(book.Title, book.Author);
        //get response as string
        var json = await response.Content.ReadAsStringAsync();

        //Asserts
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(response);
            TestContext.WriteLine($"Deleted Book Status Code: {response.StatusCode}");
        });

        //delete created book
        await _libraryHttpService.DeleteBook(book.Title, book.Author);
    }

    [Test]
    public async Task deleteBookWithoutAuthorization_returnUnauthorized()
    {
        //get first book from library
        var book = _library.SelectMany(List => List).FirstOrDefault();
        _token = null;

        //request to delete book without authorization
        HttpResponseMessage response = await _libraryHttpService.DeleteBook(_token, book.Title, book.Author);

        // Asserts
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.NotNull(response);
        });
    }

    [Test]
    public async Task DeleteBookByTitle_AuthorNotExist_ReturnNotFound()
    {
        //get first book from library
        var firstBookTitle = _library.SelectMany(list => list).FirstOrDefault()?.Title;
        //create unique author
        var Author = "Authr" + _time;
        //request to delete book
        HttpResponseMessage response = await _libraryHttpService.DeleteBook(firstBookTitle, Author);

        //Asserts
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(response);
        });
    }

    [Test]
    public async Task DeleteBookByAuthor_TitleNotExist_ReturnNotFound()
    {
        //get first book from library
        var firstBookAuthor = _library.SelectMany(list => list).FirstOrDefault()?.Author;
        //create unique title
        var Title = "Title" + _time;
        //request to delete book
        HttpResponseMessage response = await _libraryHttpService.DeleteBook(Title, firstBookAuthor);

        //Asserts
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(response);
        });
    }

    [Test]
    public async Task DeleteAllBookByTitle()
    {
        //get all books from library
        HttpResponseMessage response = await _libraryHttpService.GetAllBooks();
        //get response as string
        var json = await response.Content.ReadAsStringAsync();
        //deserialize json to list of books
        var books = JsonConvert.DeserializeObject<List<Book>>(json);

        //get count of books in library
        var booksInLibrary = books.Count;
        var deletedBooksCount = 0;

        //get books to delete by title
        var booksToDelete = books
            .Where(book => book.Title == "The Book 1")
            .ToList();

        //delete books
        foreach (var book in booksToDelete)
        {
            await _libraryHttpService.DeleteBook(book.Title, book.Author);
            deletedBooksCount++;
        }
        //get count of books after delete
        var BooksAfterDelete = books.Count - deletedBooksCount;

        //Asserts
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);
            Assert.AreEqual(6, BooksAfterDelete);
        });
    }

    [Test]
    public async Task DeleteAllBookByAuthor()
    {
       //get all books from library
        HttpResponseMessage response = await _libraryHttpService.GetAllBooks();
        //get response as string
        var json = await response.Content.ReadAsStringAsync();
        //deserialize json to list of books
        var books = JsonConvert.DeserializeObject<List<Book>>(json);
        
        //get count of books in library
        var booksInLibrary = books.Count;
        var deletedBooksCount = 0;

        //get books to delete by author
        var booksToDelete = books
            .Where(book => book.Author == "The Author 1")
            .ToList();
        //delete books
        foreach (var book in booksToDelete)
        {
            await _libraryHttpService.DeleteBook(book.Title, book.Author);
            deletedBooksCount++;
        }
        //get count of books after delete
        var BooksAfterDelete = books.Count - deletedBooksCount;

        //Asserts
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);
            Assert.AreEqual(6, BooksAfterDelete);
        });
    }

    public async Task deleteAllBook()
    {
        //get all books from library
        HttpResponseMessage response = await _libraryHttpService.GetAllBooks();
        //get response as string
        var json = await response.Content.ReadAsStringAsync();
        //deserialize json to list of books
        var books = JsonConvert.DeserializeObject<List<Book>>(json);
        //get count of books in library
        var booksInLibrary = books.Count;
        var deletedBooksCount = 0;
        //get books to delete
        var booksToDelete = books
            .Select(book => new { book.Title, book.Author })
            .ToList();
        //delete books
        foreach (var book in booksToDelete)
        {
            await _libraryHttpService.DeleteBook(book.Title, book.Author);
            deletedBooksCount++;
        }
        //get count of books after delete
        var BooksAfterDelete = books.Count - deletedBooksCount;

        //Asserts
        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);
            Assert.AreEqual(0, BooksAfterDelete);
        });
    }

    [TearDown]
    public async Task TearDown()
    {

    }
}

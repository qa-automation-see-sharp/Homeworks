using LibraryV2.Models;

namespace LibraryV2.Repositories;

public interface IBookRepository
{
    public void AddBook(Book book);
    public Book? GetBook(Func<Book, bool> condition);
    public List<Book> GetMany(Func<Book, bool> condition);
    public bool Delete(Func<Book, bool> condition);
    public bool Exists(Book book);
    public List<Book> GetAll();
}

public class BookRepository : IBookRepository
{
    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    private readonly List<Book> _books = new();

    public Book? GetBook(Func<Book, bool> condition)
    {
        var book = _books.FirstOrDefault(condition);
        Console.WriteLine(book != null ? $"Found book: {book.Title} by {book.Author}" : "Book not found");
        return book;
        //return _books.FirstOrDefault(condition);
    }


    public List<Book> GetMany(Func<Book, bool> condition)
    {
        var books = _books.Where(condition).ToList();
        Console.WriteLine(books.Any() ? $"Found {books.Count} book(s)" : "No books found");
        return books;
        //return _books.Where(condition).ToList();
    }

    public bool Delete(Func<Book, bool> condition)
    {
        var bookToRemove = _books.FirstOrDefault(condition);
        if (bookToRemove is null)
        {
            Console.WriteLine("Book to delete not found");
            return false;
        }

        _books.Remove(bookToRemove);
        Console.WriteLine($"Deleted book: {bookToRemove.Title} by {bookToRemove.Author}");
        return true;
    }

    public bool Exists(Book book)
    {
        return _books.Exists(b => b.Title == book.Title && b.Author == book.Author);
    }

    public List<Book> GetAll()
    { return _books.ToList(); }
}
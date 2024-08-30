using LibraryV2.Models;

namespace LibraryV2.Repositories;

public class BookRepository : IBookRepository
{
    private readonly List<Book> _books = new();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }


    public Book? GetBook(Func<Book, bool> condition)
    {
        return _books.FirstOrDefault(condition);
    }


    public List<Book> GetMany(Func<Book, bool> condition)
    {
        return _books.Where(condition).ToList();
    }

    public bool Delete(Func<Book, bool> condition)
    {
        var bookToRemove = _books.FirstOrDefault(condition);
        if (bookToRemove is null)
        {
            return false;
        }

        _books.Remove(bookToRemove);
        return true;
    }

    public bool Exists(Book book)
    {
        return _books.Exists(b => b.Title == book.Title && b.Author == book.Author);
    }
}
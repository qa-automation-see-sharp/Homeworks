using LibraryV2.Models;

namespace LibraryV2.Repositories;

public interface IBookRepository
{
    public void AddBook(Book book);
    public Book? GetBook(Func<Book, bool> condition);
    public List<Book> GetMany(Func<Book, bool> condition);
    public bool Delete(Func<Book, bool> condition);
    public bool Exists(Book book);
}
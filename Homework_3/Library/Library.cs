namespace Library;

public class Library
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine("Book added successfully.");
    }

    public void ViewAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books available in the library.");
            return;
        }

        foreach (var book in books)
        {
            book.DisplayBookInfo();
        }
    }

    public void SearchBooks(string query)
    {
        var results = books.Where(book => book.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                          book.Author.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("No books found matching the query.");
        }
        else
        {
            foreach (var book in results)
            {
                book.DisplayBookInfo();
            }
        }
    }

    public void BorrowBook(string title)
    {
        var book = books.FirstOrDefault(b => b.Title == title);
        if (book == null)
        {
            Console.WriteLine("Book not found.");
        }
        else if (book.IsBorrowed)
        {
            Console.WriteLine("Book is already borrowed.");
        }
        else
        {
            book.IsBorrowed = true;
            Console.WriteLine("Book borrowed successfully.");
        }
    }

    public void ReturnBook(string title)
    {
        var book = books.FirstOrDefault(b => b.Title == title);
        if (book == null)
        {
            Console.WriteLine("Book not found.");
        }
        else if (!book.IsBorrowed)
        {
            Console.WriteLine("Book is not borrowed.");
        }
        else
        {
            book.IsBorrowed = false;
            Console.WriteLine("Book returned successfully.");
        }
    }
}
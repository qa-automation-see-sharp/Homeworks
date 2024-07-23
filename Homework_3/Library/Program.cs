public class Program
{
    public static void Main()
    {
        Library library = new Library();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. View All Books");
            Console.WriteLine("3. Search Books");
            Console.WriteLine("4. Borrow Book");
            Console.WriteLine("5. Return Book");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook(library);
                    break;
                case "2":
                    library.ViewAllBooks();
                    break;
                case "3":
                    SearchBooks(library);
                    break;
                case "4":
                    BorrowBook(library);
                    break;
                case "5":
                    ReturnBook(library);
                    break;
                case "6":
                    exit = true;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void AddBook(Library library)
    {
        Console.Write("Enter book title: ");
        string title = Console.ReadLine();
        Console.Write("Enter book author: ");
        string author = Console.ReadLine();

        Book book = new Book(title, author);
        library.AddBook(book);
    }

    static void SearchBooks(Library library)
    {
        Console.Write("Enter search query (title or author): ");
        string query = Console.ReadLine();
        library.SearchBooks(query);
    }

    static void BorrowBook(Library library)
    {
        Console.Write("Enter book Title to borrow: ");
        string isbn = Console.ReadLine();
        library.BorrowBook(isbn);
    }

    static void ReturnBook(Library library)
    {
        Console.Write("Enter book Title to return: ");
        string isbn = Console.ReadLine();
        library.ReturnBook(isbn);
    }
}
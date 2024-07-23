public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsBorrowed { get; set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
        IsBorrowed = false;
    }

    public void DisplayBookInfo()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}, Borrowed: {IsBorrowed}");
    }
}
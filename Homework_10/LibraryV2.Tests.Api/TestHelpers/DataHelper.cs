using LibraryV2.Models;

namespace LibraryV2.Tests.Api.TestHelpers;

public static class DataHelper
{
    public static Book CreateBook()
    {
        return new Book
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = new Random().Next(1300, 2024)
        };
    }
    
    public static Book CreateBook(string title, string author)
    {
        return new Book
        {
            Title = title,
            Author = author,
            YearOfRelease = new Random().Next(1300, 2024)
        };
    }
}
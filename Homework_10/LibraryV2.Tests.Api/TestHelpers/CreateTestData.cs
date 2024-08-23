using LibraryV2.Models;

namespace LibraryV2.Tests.Api.TestHelpers;

public static class CreateTestData
{
    public static Book CreateNewBook()
    {
        return new Book
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            YearOfRelease = new Random().Next(1850, 2024)
        };
    }
    
    public static User CreateNewUser()
    {
        return new User
        {
            FullName = Guid.NewGuid().ToString(),
            NickName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
        };
    }
}
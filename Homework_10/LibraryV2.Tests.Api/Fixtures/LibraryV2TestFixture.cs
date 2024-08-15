using LibraryV2.Models;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{
    internal readonly LibraryHttpService _libraryHttpService = new();
    internal readonly Dictionary<User, string> _users = new();
    internal readonly List<Book> _books = new();

    [OneTimeSetUp]
    public async Task SetUp()
    {
        _libraryHttpService.Configure("http://localhost:5111");
        for (int i = 0; i < 10; i++)
        {
            User user = new()
            {
                FullName = Guid.NewGuid() + "-n" + i,
                Password = Guid.NewGuid() + "-n" + i,
                NickName = Guid.NewGuid() + "-n" + i
            };

            await _libraryHttpService.CreateUser(user);
            var token = await _libraryHttpService.LogIn(user);
            _users.Add(user, token.Token.Trim('"'));
        }

        for (int i = 0; i < 10; i++)
        {
            Book book = new()
            {
                Title = Guid.NewGuid() + "-n" + i,
                Author = Guid.NewGuid() + "-n" + i,
                YearOfRelease = 0000 + i
            };
            _books.Add(book);
            var token = _users.First().Value;
            await _libraryHttpService.CreateBook(token, book);            
        }
    }

    [OneTimeTearDown]
    public void TearDown()
    {
    }
}
using LibraryV2.Models;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{
    internal readonly LibraryHttpService _libraryService = new LibraryHttpService();
    internal readonly Dictionary<User, string> _users = new();
    internal readonly List<Book> _books = new();

    [OneTimeSetUp]
    public async Task SetUp()
    {
        _libraryService.Configure("http://localhost:5111/");
        for (int i = 1; i <= 10; i++)
        {
            User user = new()
            {
                FullName = Guid.NewGuid() + "-" + i,
                Password = Guid.NewGuid() + "-" + i,
                NickName = Guid.NewGuid() + "-" + i
            };

            await _libraryService.CreateUser(user);
            var json = await _libraryService.LogIn(user);
            var token = json.Token.Trim('"');
            _users.Add(user, token);
        }

        for (int i = 1; i <= 10; i++)
        {
            Book book = new()
            {
                Title = Guid.NewGuid() + "-" + i,
                Author = Guid.NewGuid() + "-" + i,
                YearOfRelease = 1980
            };
            _books.Add(book);
            await _libraryService.CreateBook(_users.First().Value, book);
        }
    }

    [OneTimeTearDown]
    public void TearDown()
    {
    }
}
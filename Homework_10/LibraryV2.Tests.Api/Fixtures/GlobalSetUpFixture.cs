using LibraryV2.Models;
using LibraryV2.Tests.Api.Services;
using LibraryV2.Tests.Api.TestHelpers;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Fixtures;

[SetUpFixture]
public class GlobalSetUpFixture
{
    public readonly LibraryHttpService LibraryHttpService = new();
    protected readonly Dictionary<User, AuthorizationToken> Users = new();
    protected Dictionary<string, string> _bookDetails = new();

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        LibraryHttpService.Configure("http://localhost:5111/");


        for (var i = 0; i < 3; i++)
        {
            var user = CreateTestData.CreateNewUser();
            var createUserResponse = await LibraryHttpService.CreateUser(user);
            var httpResponseMessage = await LibraryHttpService.LogIn(user);
            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            var authToken = JsonConvert.DeserializeObject<AuthorizationToken>(jsonString);

            Users.Add(user, authToken);
        }

        for (var i = 0; i < 3; i++)
        {
            var book = CreateTestData.CreateNewBook();
            var userToken = Users.First().Value.Token;
            await LibraryHttpService.CreateBook(userToken, book);
            _bookDetails.Add(book.Title, book.Author);
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine("Here is the one-time tear down");
    }
}
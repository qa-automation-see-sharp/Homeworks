using LibraryV2.Models;
using LibraryV2.Tests.Api.Services;
using Bogus;
using System.Net;

namespace LibraryV2.Tests.Api.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{
    internal LibraryHttpService LibraryHttpService = new LibraryHttpService();
    public string Token;
    public User User;
    internal Book Book;

    [OneTimeSetUp]
    public async Task SetUpAsync()
    {
        LibraryHttpService.Configure("http://localhost:5111/");

        var faker = new Faker();
        User = new User()
        {
            FullName = "Percy Smith",
            NickName = $"locksmith{faker.Random.AlphaNumeric(4)}",
            Password = "cg2ir37"
        };

        var responseUser = await LibraryHttpService.CreateUser(User);

        var responseToken = LibraryHttpService.LogIn(User);

        Token = (await responseToken.Result.Content.ReadAsStringAsync()).Trim('"');

        Book = new Book()
        {
            Title = "Grasshopper",
            Author = "Kotaro Isaka",
            YearOfRelease = 2004
        };

        try
        {
            var createdBook = await LibraryHttpService.CreateBook(Token, Book);

            var jsonString = await createdBook.Content.ReadAsStringAsync();

            if (createdBook.StatusCode == HttpStatusCode.BadRequest && jsonString.Contains("already exists"))
            {
                Console.WriteLine("Book already exists, skipping.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    [OneTimeTearDown]
    public void TearDown()
    {

    }
}
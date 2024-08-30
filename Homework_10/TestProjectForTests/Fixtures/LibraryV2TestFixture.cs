using TestProjectForTests.Services;

namespace TestProjectForTests.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{
    protected LibraryHttpService LibraryHttpService = new();

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        LibraryHttpService = new LibraryHttpService();
        LibraryHttpService.Configure("http://localhost:5111/");
        await LibraryHttpService.CreateDefaultUser();
        await LibraryHttpService.AuthorizeLikeDefaultUser();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
    }
}
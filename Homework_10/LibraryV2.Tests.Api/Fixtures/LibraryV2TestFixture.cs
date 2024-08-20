using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{
    protected LibraryHttpService HttpService = new();

    [OneTimeSetUp]
    public async Task SetUp()
    {
        HttpService.Configure("http://localhost:5111/");
        await HttpService.CreateDefaultUser();
        await HttpService.Authorize();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
    }
}
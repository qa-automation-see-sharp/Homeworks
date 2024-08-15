using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{
    protected LibraryHttpService LibraryHttpService2;
    [OneTimeSetUp]
    public void SetUp()
    {
    }

    [OneTimeTearDown]
    public void TearDown()
    {
    }
}
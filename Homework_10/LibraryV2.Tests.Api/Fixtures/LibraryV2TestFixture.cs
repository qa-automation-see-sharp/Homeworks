using LibraryV2.Models;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{
    protected LibraryHttpService _httpService = new();

    [OneTimeSetUp]
    public async Task SetUp()
    {
    }

    [OneTimeTearDown]
    public void TearDown()
    {
    }
}
using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Tests;
using Microsoft.Extensions.Logging;

namespace LibraryV2.Tests.Api.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{

    public ILogger<LibraryV2TestFixture> _logger;

    public LibraryV2TestFixture()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = loggerFactory.CreateLogger<LibraryV2TestFixture>();

    }
    [OneTimeSetUp]
    public new async Task SetUp()
    {
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
    }
}
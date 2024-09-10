using LibraryV2.Tests.Api.Services;
using LibraryV2.Models;
using LibraryV2.Tests.Api.Tests;
using Microsoft.Extensions.Logging;

namespace LibraryV2.Tests.Api.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{
    protected readonly ILogger<LibraryV2TestFixture> Logger;

    public LibraryV2TestFixture()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        Logger = loggerFactory.CreateLogger<LibraryV2TestFixture>();
    }
}
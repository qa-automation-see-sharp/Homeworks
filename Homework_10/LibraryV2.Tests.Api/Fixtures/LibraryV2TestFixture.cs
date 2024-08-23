using LibraryV2.Models;
using LibraryV2.Tests.Api.Services;
using Bogus;
using System.Net;

namespace LibraryV2.Tests.Api.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{

    [OneTimeSetUp]
    public async Task SetUpAsync()
    {
    }

    [OneTimeTearDown]
    public void TearDown()
    {

    }
}
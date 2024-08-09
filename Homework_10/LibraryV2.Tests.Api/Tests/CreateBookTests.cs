using LibraryV2.Tests.Api.Fixtures;
using LibraryV2.Tests.Api.Services;

namespace LibraryV2.Tests.Api.Tests;

public class CreateBookTests : LibraryV2TestFixture
{
    private LibraryHttpService _libraryHttpService;
    
    [SetUp]
    public new void SetUp()
    {
        _libraryHttpService = new LibraryHttpService();
        _libraryHttpService.Configure("http://localhost:5111/");
    }
    
    //TODO cover with tests all endpoints from Books controller
    // Create book
}
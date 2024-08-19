using LibraryV2.Models;
using LibraryV2.Tests.Api.Services;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Fixtures;

[TestFixture]
public class LibraryV2TestFixture : GlobalSetUpFixture
{
    protected LibraryHttpService HttpService = new();

    [OneTimeSetUp]
    public async Task SetUp()
    {
        // Виніс конфігурацію сюди, щоб не дублювати в кожному тесті
        // так як ті використовуєш один і той же сервіс у всіх тестах
        HttpService.Configure("http://localhost:5111/");
        await HttpService.CreateDefaultUser();
        await HttpService.Authorize();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
    }
}
namespace LibraryV2.Tests.Api.Fixtures;

[SetUpFixture]
public class GlobalSetUpFixture
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Console.WriteLine("Here is the global setup");
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine("Here is the global tear down");
    }
}
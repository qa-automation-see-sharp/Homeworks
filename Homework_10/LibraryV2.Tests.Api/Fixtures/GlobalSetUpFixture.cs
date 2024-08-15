namespace LibraryV2.Tests.Api.Fixtures;

[SetUpFixture]
public class GlobalSetUpFixture
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Console.WriteLine("Here is the one-time setup");
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine("Here is the one-time tear down");
    }
}
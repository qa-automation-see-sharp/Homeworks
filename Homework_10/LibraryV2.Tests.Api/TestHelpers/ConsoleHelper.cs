
namespace LibraryV2.Tests.Api.TestHelpers
{
    public static class ConsoleHelper
    {
        public static string FormatStringTable = "{0,-40}";
        public static void PostInfo(string name, string url, string jsonString, HttpResponseMessage response)
        {
            // Console.Write(
            //     $"{name}:\n" +
            //     $"POST request to:\t{url}\n" +
            //     $"Content:\t{jsonString}\n" +
            //     $"Response status code is :\t{response.StatusCode}"
            //     );
            Console.WriteLine($"{name}:");
            Console.WriteLine($"POST request to: {url}");
            Console.WriteLine($"Content: {jsonString}");
            Console.WriteLine($"Response status code is : {response.StatusCode}");
        }
        
        public static void GetInfo(string name, string url, string jsonString, HttpResponseMessage response){
            Console.WriteLine($"{name}:");
            Console.WriteLine($"GET request to: {url}");
            Console.WriteLine($"Content: {jsonString}");
            Console.WriteLine($"Response status code is : {response.StatusCode}");
        }
    }
}
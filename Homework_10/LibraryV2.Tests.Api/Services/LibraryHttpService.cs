using System.Text;
using LibraryV2.Models;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Services;

public class LibraryHttpService
{
    private readonly HttpClient _httpClient;
    
    private User? DefaultUser { get; set; }
    public AuthorizationToken? AuthToken { get; set; }

    public LibraryHttpService()
    {
        _httpClient = new HttpClient();
    }

    public LibraryHttpService Configure(string baseUrl)
    {
        _httpClient.BaseAddress = new Uri(baseUrl);
        return this;
    }

    public async Task<LibraryHttpService> CreateDefaultUser()
    {
        DefaultUser = new User
        {
            NickName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            FullName = Guid.NewGuid().ToString()
        };
        
        var url = EndpointsForTest.Users.Register;
        var json = JsonConvert.SerializeObject(DefaultUser);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var jsonString = await response.Content.ReadAsStringAsync();
        
         Console.WriteLine($"Created default user:\n{jsonString}");
        
         return this;
    }
    public async Task<LibraryHttpService> Authorize()
    {
        var url = EndpointsForTest.Users.Login + $"?nickname={DefaultUser.NickName}&password={DefaultUser.Password}";
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        AuthToken = JsonConvert.DeserializeObject<AuthorizationToken>(content);

        Console.WriteLine($"Authorized with user:\n{content}");
        return this;
    }

    public string? GetDefaultUserToken()
    {
        return AuthToken?.Token;
    }
 
    public async Task<HttpResponseMessage> CreateUser(User user)
    {
        var url = EndpointsForTest.Users.Register;
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var jsonString = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine($"Created user:\n{jsonString}");

        return response;
    }
    
    public async Task<HttpResponseMessage> LogIn(User user)
    {
        var url = EndpointsForTest.Users.Login + $"?nickname={user.NickName}&password={user.Password}";
        var response = await _httpClient.GetAsync(url);
        var jsonString = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine($"Logged in with:\n{jsonString}");

        return response;
    }
    
    public async Task<HttpResponseMessage> PostBook(string token, Book book)
    {
        var url = EndpointsForTest.Books.Create + $"?token={token}";
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var jsonString = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine($"POST request to:\n{_httpClient.BaseAddress}{url}");
        Console.WriteLine($"Response Status Code is: {response.StatusCode}"); 
        Console.WriteLine($"Body: {jsonString}");  

        return response;
    }
    public async Task<HttpResponseMessage> PostBook(Book book)
    {
        var url = EndpointsForTest.Books.Create + $"?token={AuthToken.Token}";
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var jsonString = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine($"POST request to:\n{_httpClient.BaseAddress}{url}");
        Console.WriteLine($"Response Status Code is: {response.StatusCode}"); 
        Console.WriteLine($"Body: {jsonString}");  

        return response;
    }
    
    public async Task<HttpResponseMessage> GetBooksByTitle(string title)
    {
        var url = EndpointsForTest.Books.GetBooksByTitle + title;
        var response = await _httpClient.GetAsync(url);
        var jsonString = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine($"GET request to:\n{_httpClient.BaseAddress}{url}");
        Console.WriteLine($"Response Status Code is: {response.StatusCode}"); 
        Console.WriteLine($"Content: {jsonString}");  

        return response;
    }
    
    public async Task<HttpResponseMessage> GetBooksByAuthor(string author)
    {
        var url = EndpointsForTest.Books.GetBooksByAuthor(author);
        var response = await _httpClient.GetAsync(url);
        var jsonString = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine($"GET request to:\n{_httpClient.BaseAddress}{url}");
        Console.WriteLine($"Response Status Code is: {response.StatusCode}"); 
        Console.WriteLine($"Content: {jsonString}");  

        return response;
    }
    
    public async Task<HttpResponseMessage> DeleteBook(string token, string title, string author)
    {
        var url = EndpointsForTest.Books.Delete(title, author, token);
        var response = await _httpClient.DeleteAsync(url);
        var jsonString = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine($"Delete request to:\n{_httpClient.BaseAddress}{url}");
        Console.WriteLine($"Response Status Code is: {response.StatusCode}"); 
        Console.WriteLine($"Content: {jsonString}");  

        return response;
    }
}
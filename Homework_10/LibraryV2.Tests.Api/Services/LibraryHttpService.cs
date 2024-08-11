using System.Text;
using LibraryV2.Models;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Services;

public class LibraryHttpService
{
    private readonly HttpClient _httpClient;

    public LibraryHttpService()
    {
        _httpClient = new HttpClient();
    }

    public void Configure(string baseUrl)
    {
        _httpClient.BaseAddress = new Uri(baseUrl);
    }
    

    public async Task<User> CreateUser(User user)
    {
        var url = ApiEndpoints.Users.Register;
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
         var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<User>(jsonString);
    }

    public async Task<AuthorizationToken> LogIn(User user)
    {
        var url = ApiEndpoints.Users.Login + $"?nickName={user.NickName}&password={user.Password}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();

        return new AuthorizationToken { Token = jsonString };
    }
    

     public async Task<Book?> CreateBook(string token, Book book)
    {
        var url = ApiEndpoints.Books.Create + $"?token={token}";
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Book>(jsonString);
    }
    
    public async Task<List<Book>> GetBooksByTitle(string title)
    {
        var url = ApiEndpoints.Books.GetBooksByTitle.Replace("{title}", title);
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<Book>>(jsonString);
    }
    
    public async Task<List<Book>> GetBooksByAuthor(string author)
    {
        var url = ApiEndpoints.Books.GetBooksByAuthor.Replace("{author}", author);;
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<Book>>(json);
    }
    
    public async Task<string> DeleteBook(string token, string title, string author)
    {
        var url = ApiEndpoints.Books.Delete + $"?title={title}&author={author}&token={token}";
        var response = await _httpClient.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<string>(jsonString);
    }
}
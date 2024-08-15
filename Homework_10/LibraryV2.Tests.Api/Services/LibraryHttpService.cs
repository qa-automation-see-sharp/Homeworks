using System.Net.Http.Headers;
using System.Text;
using LibraryV2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    

    public async Task<HttpResponseMessage> CreateUser(User user)
    {
        var url = ApiEndpoints.Users.Register;
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        return response;
    }
    
    public async Task<HttpResponseMessage> LogIn(User user)
    {
        var url = ApiEndpoints.Users.Login + $"?nickname={user.NickName}&password={user.Password}";
        var response = await _httpClient.GetAsync(url);

        return response;
    }
    

    public async Task<HttpResponseMessage> CreateBook(string token, Book book)
    {
        var url = ApiEndpoints.Books.Create + $"?token={token}";
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        return response;
    }
    
    public async Task<HttpResponseMessage> GetBooksByTitle(string title)
    {
        var url = ApiEndpoints.Books.GetBooksByTitle.Replace("{title}", title);
        var response = await _httpClient.GetAsync(url);

        return response;
    }
    
    public async Task<HttpResponseMessage> GetBooksByAuthor(string author)
    {
        var url = ApiEndpoints.Books.GetBooksByAuthor.Replace("{author}", author);
        var response = await _httpClient.GetAsync(url);

        return response;
    }

    public async Task<HttpResponseMessage> GetAllBooks()
    {
        var url = ApiEndpoints.Books.GetAll;
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        return response;
    }
    
    
    public async Task<HttpResponseMessage> DeleteBook(string token, string title, string author)
    {
        var url = ApiEndpoints.Books.Delete + $"?title={title}&author={author}&token={token}";
        var response = await _httpClient.DeleteAsync(url);

        return response;
    }
}
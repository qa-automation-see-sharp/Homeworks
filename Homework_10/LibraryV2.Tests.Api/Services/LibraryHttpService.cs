using System.Text;
using LibraryV2.Models;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Services;

//TODO fix endpoints as we've done it on the last lesson, I've created a new class TestApiEndpoints for this  
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
        var url = TestApiEndpoints.Users.Register;
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        return response;
    }
    
    public async Task<HttpResponseMessage> LogIn(User user)
    {
        var url = TestApiEndpoints.Users.Login(user.NickName, user.Password);
        var response = await _httpClient.GetAsync(url);

        return response;
    }
    

    public async Task<HttpResponseMessage> CreateBook(string token, Book book)
    {
        var url = TestApiEndpoints.Books.Create(token);
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        return response;
    }
    
    public async Task<HttpResponseMessage> GetBooksByTitle(string title)
    {
        var url = TestApiEndpoints.Books.GetBooksByTitle(title);
        var response = await _httpClient.GetAsync(url);

        return response;
    }
    
    public async Task<HttpResponseMessage> GetBooksByAuthor(string author)
    {
        var url = TestApiEndpoints.Books.GetBooksByAuthor(author);
        var response = await _httpClient.GetAsync(url);

        return response;
    }
    
    public async Task<HttpResponseMessage> DeleteBook(string token, string title, string author)
    {
        var url = TestApiEndpoints.Books.Delete(title, author, token);
        var response = await _httpClient.DeleteAsync(url);

        return response;
    }
}
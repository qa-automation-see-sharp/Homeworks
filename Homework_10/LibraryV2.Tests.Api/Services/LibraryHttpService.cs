using System.Text;
using Bogus;
using LibraryV2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LibraryV2.Tests.Api.Services;

public class LibraryHttpService
{
    private readonly HttpClient _httpClient;
    private User? DefaultUser { get; set; }
    private AuthorizationToken? AuthorizationToken { get; set; }

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
        var faker = new Faker();

        DefaultUser = new User()
        {
            FullName = "David Solis",
            NickName = $"soledavi{faker.Random.AlphaNumeric(4)}",
            Password = "126rtgc"
        };

        var url = EndpointsForTest.Users.Register;
        var json = JsonConvert.SerializeObject(DefaultUser);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        return this;
    }

    public async Task<LibraryHttpService> Authorize()
    {
        var url = EndpointsForTest.Users.Login + $"?nickname={DefaultUser.NickName}&password={DefaultUser.Password}";
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        AuthorizationToken = JsonConvert.DeserializeObject<AuthorizationToken>(content);

        return this;
    }

    public async Task<HttpResponseMessage> CreateUser(User user)
    {
        var url = EndpointsForTest.Users.Register;
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        return response;
    }
    
    public async Task<HttpResponseMessage> LogIn(User user)
    {
        var url = EndpointsForTest.Users.Login + $"?nickname={user.NickName}&password={user.Password}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return response;
    }
    

    public async Task<HttpResponseMessage> CreateBook(string token, Book book)
    {
        var url = EndpointsForTest.Books.Create(token);
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        return response;
    }
    public async Task<HttpResponseMessage> CreateBook(Book book)
    {
        var url = EndpointsForTest.Books.Create(AuthorizationToken.Token);
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        return response;
    }

    public async Task<HttpResponseMessage> GetBooksByTitle(string title)
    {
        var url = EndpointsForTest.Books.GetBooksByTitle(title);
        var uri = new Uri(_httpClient.BaseAddress, url);
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        return response;
    }
    
    public async Task<HttpResponseMessage> GetBooksByAuthor(string author)
    {
        var url = EndpointsForTest.Books.GetBooksByAuthor(author);
        var uri = new Uri(_httpClient.BaseAddress, url);
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        return response;
    }
    
    public async Task<HttpResponseMessage> DeleteBook(string token, string title, string author)
    {
        var url = EndpointsForTest.Books.Delete(title, author, token);
        var response = await _httpClient.DeleteAsync(url);
        response.EnsureSuccessStatusCode();

        return response;
    }
    public async Task<HttpResponseMessage> DeleteBook(string title, string author)
    {
        var url = EndpointsForTest.Books.Delete(title, author, AuthorizationToken.Token);
        var response = await _httpClient.DeleteAsync(url);
        response.EnsureSuccessStatusCode();

        return response;
    }
}
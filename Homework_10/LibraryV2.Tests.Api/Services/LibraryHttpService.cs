using System;
using System.Net;
using System.Text;
using LibraryV2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LibraryV2.Tests.Api.Services;

public class LibraryHttpService
{
    private readonly HttpClient _httpClient;
    private User? _testUser { get; set; }
    private AuthorizationToken? _authorizationToken { get; set; }
    private Book? _testBookNonExist { get; set; }

    public LibraryHttpService()
    {
        _httpClient = new HttpClient();
    }

    public LibraryHttpService Configure(string baseUrl)
    {
        _httpClient.BaseAddress = new Uri(baseUrl);
        return this;
    }

    public async Task<LibraryHttpService> CreateTestUser()
    {
        _testUser = new User()
        {
            FullName = "123",
            NickName = "qwerty",
            Password = "asdf"
        };

        var endpointUrl = EndpointsForTest.Users.Register;
        var json = JsonConvert.SerializeObject(_testUser);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpointUrl, content);
        var jsonString = await response.Content.ReadAsStringAsync();

        TestContext.WriteLine($"Create User: {jsonString}");
        return this;
    }

    

    public async Task<LibraryHttpService> LogInTestUser()
    {
        var endpointUrl = EndpointsForTest.Users.Login + $"?nickname={_testUser.NickName}&password={_testUser.Password}";
        var response = await _httpClient.GetAsync(endpointUrl);
        var content = await response.Content.ReadAsStringAsync();
        _authorizationToken = JsonConvert.DeserializeObject<AuthorizationToken>(content);
        var jsonString = await response.Content.ReadAsStringAsync();
        return this;
    }

    

    public async Task<HttpResponseMessage> CreateUser(User user)
    {
        var url = EndpointsForTest.Users.Register;
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var jsonString = await response.Content.ReadAsStringAsync();
        WriteLog(response.RequestMessage.Method.ToString(), _httpClient.BaseAddress, url, response.StatusCode, jsonString);

        return response;
    }

    public async Task<HttpResponseMessage> LogIn(User user)
    {
        var url = EndpointsForTest.Users.Login + $"?nickname={user.NickName}&password={user.Password}";
        var response = await _httpClient.GetAsync(url);
        var jsonString = await response.Content.ReadAsStringAsync();
        WriteLog(response.RequestMessage.Method.ToString(), _httpClient.BaseAddress, url, response.StatusCode, jsonString);

        return response;
    }


    public async Task<HttpResponseMessage> CreateBook(string token, Book book)
    {
        var url = EndpointsForTest.Books.Create(token);
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var jsonString = await response.Content.ReadAsStringAsync();
        WriteLog(response.RequestMessage.Method.ToString(), _httpClient.BaseAddress, url, response.StatusCode, jsonString);

        return response;
    }
    public async Task<HttpResponseMessage> CreateBook(Book book)
    {
        var url = EndpointsForTest.Books.Create(_authorizationToken.Token);
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var jsonString = await response.Content.ReadAsStringAsync();
        WriteLog(response.RequestMessage.Method.ToString(), _httpClient.BaseAddress, url, response.StatusCode, jsonString);

        return response;
    }

    public async Task<HttpResponseMessage> GetBooksByTitle(string title)
    {
        var url = EndpointsForTest.Books.GetBooksByTitle(title);
        var uri = new Uri(_httpClient.BaseAddress, url);
        var response = await _httpClient.GetAsync(uri);
        var jsonString = await response.Content.ReadAsStringAsync();
        WriteLog(response.RequestMessage.Method.ToString(), _httpClient.BaseAddress, url, response.StatusCode, jsonString);

        return response;
    }

    public async Task<HttpResponseMessage> GetBooksByAuthor(string author)
    {
        var url = EndpointsForTest.Books.GetBooksByAuthor(author);
        var uri = new Uri(_httpClient.BaseAddress, url);
        var response = await _httpClient.GetAsync(uri);
        var jsonString = await response.Content.ReadAsStringAsync();
        WriteLog(response.RequestMessage.Method.ToString(), _httpClient.BaseAddress, url, response.StatusCode, jsonString);

        return response;
    }

    public async Task<HttpResponseMessage> GetAllBooks()
    {
        var url = EndpointsForTest.Books.GetAll;
        var uri = new Uri(_httpClient.BaseAddress, url);
        var response = await _httpClient.GetAsync(uri);
        var jsonString = await response.Content.ReadAsStringAsync();
        WriteLog(response.RequestMessage.Method.ToString(), _httpClient.BaseAddress, url, response.StatusCode, jsonString);

        return response;
    }

    public async Task<HttpResponseMessage> DeleteBook(string token, string title, string author)
    {
        var url = EndpointsForTest.Books.Delete(title, author, token);
        var response = await _httpClient.DeleteAsync(url);
        var jsonString = await response.Content.ReadAsStringAsync();
        WriteLog(response.RequestMessage.Method.ToString(), _httpClient.BaseAddress, url, response.StatusCode, jsonString);

        return response;
    }
    public async Task<HttpResponseMessage> DeleteBook(string title, string author)
    {
        var url = EndpointsForTest.Books.Delete(title, author, _authorizationToken.Token);
        var response = await _httpClient.DeleteAsync(url);
        var jsonString = await response.Content.ReadAsStringAsync();
        WriteLog(response.RequestMessage.Method.ToString(), _httpClient.BaseAddress, url, response.StatusCode, jsonString);

        return response;
    }

    

    public void WriteLog(string method, Uri baseAdress, string url, HttpStatusCode statusCode, string content)
    {
        Console.WriteLine(method + " request to: " + baseAdress + url);
        Console.WriteLine("Response status code: " + statusCode);
        Console.WriteLine("Content: " + content);
    }
}
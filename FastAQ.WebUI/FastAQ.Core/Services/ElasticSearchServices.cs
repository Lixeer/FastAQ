using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using FastAQ.Models.Config.ElasticSearchConfig;
using FastAQ.Models.ESEntitys;
using Microsoft.Extensions.Options;

namespace FastAQ.Services.ElasticSearchServices;

public class ESHTTPClient
{
    private readonly string _url;
    private readonly ElasticSearchConfig _config;
    private readonly HttpClient _httpClient;

    public ESHTTPClient(IOptions<ElasticSearchConfig> options)
    {
        _config = options.Value;
        _url = _config.ConnectionUrl;
        _httpClient = new HttpClient();
        
        // 设置基本认证（如果有）
        if (!string.IsNullOrEmpty(_config.httpAuth.Username) && !string.IsNullOrEmpty(_config.httpAuth.Password))
        {
            var authToken = Encoding.ASCII.GetBytes($"{_config.httpAuth.Username}:{_config.httpAuth.Password}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
        }
        
        // 设置默认请求头
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.Timeout = TimeSpan.FromSeconds(_config.TimeoutSeconds);
    }

    // GET 请求
    public async Task<string> GetAsync(string endpoint = "")
    {
        return await GetAsync<string>(endpoint);
    }

    public async Task<T> GetAsync<T>(string endpoint = "")
    {
        var url = BuildUrl(endpoint);
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content) ?? throw new Exception("Failed to deserialize response");
    }

    // POST 请求
    public async Task<string> PostAsync(string endpoint, object data)
    {
        return await PostAsync<string>(endpoint, data);
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        var url = BuildUrl(endpoint);
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
        return JsonSerializer.Deserialize<T>(responseContent) ?? throw new Exception("Failed to deserialize response");
    }

    // PUT 请求
    public async Task<string> PutAsync(string endpoint, object data)
    {
        return await PutAsync<string>(endpoint, data);
    }

    public async Task<T> PutAsync<T>(string endpoint, object data)
    {
        var url = BuildUrl(endpoint);
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PutAsync(url, content);
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent) ?? throw new Exception("Failed to deserialize response");
    }

    // DELETE 请求
    public async Task<string> DeleteAsync(string endpoint)
    {
        return await DeleteAsync<string>(endpoint);
    }

    public async Task<T> DeleteAsync<T>(string endpoint)
    {
        var url = BuildUrl(endpoint);
        var response = await _httpClient.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content) ?? throw new Exception("Failed to deserialize response");
    }

    // HEAD 请求
    public async Task<HttpResponseMessage> HeadAsync(string endpoint)
    {
        var url = BuildUrl(endpoint);
        var request = new HttpRequestMessage(HttpMethod.Head, url);
        return await _httpClient.SendAsync(request);
    }

    // 构建完整的 URL
    private string BuildUrl(string endpoint)
    {
        if (string.IsNullOrEmpty(endpoint))
            return _url;
        
        return _url.EndsWith("/") ? $"{_url}{endpoint}" : $"{_url}/{endpoint}";
    }

    // 设置请求头（仿照 requests 的 headers 参数）
    public void SetHeader(string name, string value)
    {
        if (_httpClient.DefaultRequestHeaders.Contains(name))
        {
            _httpClient.DefaultRequestHeaders.Remove(name);
        }
        _httpClient.DefaultRequestHeaders.Add(name, value);
    }

    // 设置认证（仿照 requests 的 auth 参数）
    public void SetBasicAuth(string username, string password)
    {
        var authToken = Encoding.ASCII.GetBytes($"{username}:{password}");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
    }

    // 设置超时时间（仿照 requests 的 timeout 参数）
    public void SetTimeout(TimeSpan timeout)
    {
        _httpClient.Timeout = timeout;
    }

    // 释放资源
    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}

public class ElasticSearchServices
{
    private readonly ESHTTPClient _eSHTTPClient;
    public ElasticSearchServices(IOptions<ElasticSearchConfig> options)
    {
        _eSHTTPClient = new ESHTTPClient(options);
    }
    public async Task<T> SearchAsync<T>(string index_name, string keyword)
    {

        return await _eSHTTPClient.GetAsync<T>(endpoint: $"_search?q=question:{keyword}");

    }

    public async Task<T> InsertAsync<T>(string index_name, AQDocument doc)
    {
        return await _eSHTTPClient.PostAsync<T>(endpoint: $"{index_name}/_doc", data: doc);
    }

    public async Task<T> DeleteIndexAsync<T>(string index_name)
    {
        return await _eSHTTPClient.DeleteAsync<T>(endpoint: $"{index_name}");

    }
    public async Task<T> DeleteDocAsync<T>(string index_name, object query)
    {
        var result = await _eSHTTPClient.PostAsync<T>(endpoint: $"{index_name}/_delete_by_query", query);
        return result;
    }

}
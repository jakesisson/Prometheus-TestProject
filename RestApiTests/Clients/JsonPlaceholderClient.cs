using System;
using Microsoft.Extensions.Configuration;
using RestSharp;

public class JsonPlaceholderClient
{
    private readonly RestClient _client;

    public JsonPlaceholderClient()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("Config/appsettings.json")
            .Build();

        var baseUrl = config["BaseUrl"] ?? throw new ArgumentNullException("BaseUrl");
        _client = new RestClient(baseUrl);
    }

    public RestResponse GetPost(int id) => _client.Execute(new RestRequest($"posts/{id}", Method.Get));
    public RestResponse CreatePost(object body) => _client.Execute(new RestRequest("posts", Method.Post).AddJsonBody(body));
    public RestResponse UpdatePost(int id, object body) => _client.Execute(new RestRequest($"posts/{id}", Method.Put).AddJsonBody(body));
    public RestResponse Patch(string resource, int id, object body) => _client.Execute(new RestRequest($"{resource}/{id}", Method.Patch).AddJsonBody(body));

    public RestResponse GetSingle(string resource, int id) => _client.Execute(new RestRequest($"{resource}/{id}", Method.Get));
    public RestResponse Create(string resource, object body) => _client.Execute(new RestRequest(resource, Method.Post).AddJsonBody(body));
    public RestResponse GetWithQuery(string resource, string query) => _client.Execute(new RestRequest($"{resource}?{query}", Method.Get));
    public RestResponse Get(string path) => _client.Execute(new RestRequest(path));
}
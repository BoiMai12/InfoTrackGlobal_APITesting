using Libraries.StepDefinitions.GlobalSteps.Http.Model;
using RestSharp;
using System.Net.Mime;
using System.Reflection.PortableExecutable;

namespace Libraries.Support.Http;

public class HttpService : IHttpService
{
    public RestResponse Get(string baseUrl, string path)
    {
        var client = new RestClient(baseUrl);
        var request = new RestRequest(path);
        return client.Get(request);
    }
    public async Task<RestResponse> Get(string? baseUrl, string path, List<HttpRequestHeader>? headers)
    {
        var restClientOptions = new RestClientOptions(baseUrl ?? throw new ArgumentNullException(nameof(baseUrl)))
        {
            ThrowOnAnyError = false,
        };
        var client = new RestClient(restClientOptions);
        var request = new RestRequest(path)
        {
            Timeout = TimeSpan.FromMinutes(10)
        };
        if (headers != null)
        {
            foreach (var item in headers)
                request.AddHeader(item.Name, item.Value);
        }
        return await client.ExecuteGetAsync<ResponseModel>(request);
    }

    public async Task<RestResponse> Post(string? baseUrl, string path, DataFormat contentType, string? content, List<HttpRequestHeader>? headers)
    {
        var restClientOptions = new RestClientOptions(baseUrl ?? throw new ArgumentNullException(nameof(baseUrl)))
        {
            ThrowOnAnyError = false,
        };
        var client = new RestClient(restClientOptions);

        var request = new RestRequest(path, Method.Post)
        {
            Timeout = TimeSpan.FromMinutes(10),
            RequestFormat = contentType
        };
        if (!string.IsNullOrWhiteSpace(content))
        {
            request.AddStringBody(content, contentType);
        }
        if (headers != null)
        {
            foreach (var item in headers)
                request.AddHeader(item.Name, item.Value);
        }
        return await client.ExecutePostAsync<ResponseModel>(request);
    }

}


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

    public RestResponse Get(string? baseUrl, string path, List<HttpQueryParameter>? queryParams)
    {
        var restClientOptions = new RestClientOptions(baseUrl ?? throw new ArgumentNullException(nameof(baseUrl)));
        var client = new RestClient(restClientOptions);
        var request = new RestRequest(path)
        {
            Timeout = TimeSpan.FromMinutes(10)
        };
        foreach (var item in queryParams!) request.AddQueryParameter(item.Name!, item.Value);
        RestResponse response = null!;
        bool errorNotReceived;
        do
        {
            try
            {
                response = client.Get(request);
                errorNotReceived = true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
                errorNotReceived = false;
            }
        } while (errorNotReceived != true);

        return response;
    }

    public RestResponse Post(string? baseUrl, string path, DataFormat contentType, string? content)
    {
        var restClientOptions = new RestClientOptions(baseUrl ?? throw new ArgumentNullException(nameof(baseUrl)));
        var client = new RestClient(restClientOptions);
        var request = new RestRequest(path)
        {
            Timeout = TimeSpan.FromMinutes(10),
            RequestFormat = contentType
        };
        request.AddBody(content ?? throw new ArgumentNullException(nameof(content)));
        return client.Post(request);
    }

    public RestResponse Post(string? baseUrl, string path, DataFormat contentType, string? content,
        List<HttpRequestHeader>? headers)
    {
        var restClientOptions = new RestClientOptions(baseUrl ?? throw new ArgumentNullException(nameof(baseUrl)));
        var client = new RestClient(restClientOptions);
        var request = new RestRequest(path)
        {
            Timeout = TimeSpan.FromMinutes(10),
            RequestFormat = contentType
        };
        request.AddBody(content ?? throw new ArgumentNullException(nameof(content)));
        if (headers == null) return client.Post(request);
        foreach (var item in headers)
            request.AddHeader(item.Name, item.Value);
        return client.Post(request);
    }

    public RestResponse Delete(string? baseUrl, string path, List<HttpQueryParameter>? queryParams)
    {
        var restClientOptions = new RestClientOptions(baseUrl ?? throw new ArgumentNullException(nameof(baseUrl)));
        var client = new RestClient(restClientOptions);
        var request = new RestRequest(path)
        {
            Timeout = TimeSpan.FromMinutes(10)
        };
        foreach (var item in queryParams!) request.AddQueryParameter(item.Name!, item.Value);
        RestResponse response = null!;
        bool errorNotReceived;
        do
        {
            try
            {
                response = client.Delete(request);
                errorNotReceived = true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
                errorNotReceived = false;
            }
        } while (errorNotReceived != true);

        return response;
    }
}


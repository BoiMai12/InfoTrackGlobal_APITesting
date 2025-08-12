using RestSharp;

namespace Libraries.Support.Http
{
    public class HttpService : IHttpService
    {
        public RestResponse Get(string baseUrl, string path) 
        { 
            var client = new RestClient(baseUrl);
            var request = new RestRequest(path, Method.Get);
            return client.Get(request);
        }

        public RestResponse Get(string? baseUrl, string path, List<HttpQueryParameter>? queryParams)
        {
            var restClientOptions= new RestClientOptions(baseUrl ?? throw new ArgumentNullException(nameof(baseUrl)));
            var client = new RestClient(restClientOptions);
            var request = new RestRequest(path)
            {
                Timeout = TimeSpan.FromMinutes(10)
            };
            if (queryParams != null)
            {
                foreach (var param in queryParams)
                {
                    request.AddParameter(param.Name, param.Value);
                }
            }
            return client.Get(request);
        }

        public RestResponse Post(string? baseUrl, string path, DataFormat contentType, string? content)
        {
            var restClientOptions = new RestClientOptions(baseUrl ?? throw new ArgumentNullException(nameof(baseUrl)));
            var client = new RestClient(restClientOptions);
            var request = new RestRequest(path, Method.Post);
            request.AddHeader("Content-Type", contentType.ToString());
            request.AddBody(content ?? throw new ArgumentNullException(nameof(content)));

            return client.Post(request);
        }
        public RestResponse Post(string? baseUrl, string path, DataFormat contentType, string? content,
            List<HttpRequestHeader>? headers)
        {
            var restClientOptions = new RestClientOptions(baseUrl ?? throw new ArgumentNullException(nameof(baseUrl)));
            var client = new RestClient(restClientOptions);
            var request = new RestRequest(path, Method.Post);
            request.AddHeader("Content-Type", contentType.ToString());
            request.AddBody(content ?? throw new ArgumentNullException(nameof(content)));

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Name, header.Value);
                }
            }
            return client.Execute(request);
        }
    }
}

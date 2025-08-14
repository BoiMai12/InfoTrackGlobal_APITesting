using RestSharp;

namespace Libraries.Support.Http;

    public interface IHttpService
    {
        public RestResponse Get(string baseUrl, string path);
        public RestResponse Get(string baseUrl, string path, List<HttpQueryParameter>? queryParams);

        public RestResponse Post(string baseUrl, string path, DataFormat contentType, string? content);
        public RestResponse Post(string? baseUrl, string path, DataFormat contentType, string? content, List<HttpRequestHeader>? headers);

}


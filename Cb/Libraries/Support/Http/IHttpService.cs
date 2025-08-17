using Libraries.StepDefinitions.GlobalSteps.Http.Model;
using RestSharp;

namespace Libraries.Support.Http;

public interface IHttpService
{
   public RestResponse Get(string baseUrl, string path);

    Task<RestResponse> Post(string? baseUrl, string path, DataFormat contentType, string? content, List<HttpRequestHeader>? headers);
    Task<RestResponse> Put(string? baseUrl, string path, DataFormat contentType, string? content, List<HttpRequestHeader>? headers);
    Task<RestResponse> Delete(string? baseUrl, string path, DataFormat contentType, string? content, List<HttpRequestHeader>? headers);
    Task<RestResponse> Delete(string? baseUrl, string path, List<HttpRequestHeader>? headers);
    Task<RestResponse> Get(string? baseUrl, string path, List<HttpRequestHeader>? headers);

}


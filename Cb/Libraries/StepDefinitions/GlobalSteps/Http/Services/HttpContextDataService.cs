using RestSharp;
using Reqnroll;
using Libraries.StepDefinitions.GlobalSteps.Http.Model;
using Libraries.Support.Constrains;
using Libraries.Support.Context;

namespace Libraries.StepDefinitions.GlobalSteps.Http.Services;

public class HttpContextDataService : IHttpContextDataService
{
    private readonly FeatureContext _featureContext;
    private readonly ScenarioContext _scenarioContext;
    public HttpContextDataService(
        FeatureContext featureContext, 
        ScenarioContext scenarioContext)
    {
        _featureContext = featureContext;
        _scenarioContext = scenarioContext;
    }
    public void AddHttpBaseData(HttpBaseRequestContextData data)
    {
        _featureContext.Remove(ContextDataKeys.HttpBaseRequest);
        ContextDataHelper.Add(ContextDataKeys.HttpBaseRequest, data, _featureContext);
    }

    public void AddCurrentHttpData(HttpRequestContextData data)
    {
        _scenarioContext.Remove(ContextDataKeys.HttpCurrentRequest);
        ContextDataHelper.Add(ContextDataKeys.HttpCurrentRequest, data, _scenarioContext);
    }

    public void AddCurrentHttpResponse(RestResponse restResponse)
    {
        _scenarioContext.Remove(ContextDataKeys.HttpCurrentResponse);
        ContextDataHelper.Add(ContextDataKeys.HttpCurrentResponse, restResponse, _scenarioContext);
    }

    public HttpBaseRequestContextData GetHttpBaseData()
    {
        return ContextDataHelper.Get<HttpBaseRequestContextData>(
            ContextDataKeys.HttpBaseRequest,
            _featureContext,
            _scenarioContext);
    }

    public HttpRequestContextData GetCurrentHttpData()
    {
        return ContextDataHelper.Get<HttpRequestContextData>(
            ContextDataKeys.HttpCurrentRequest,
            _featureContext,
            _scenarioContext);
    }
    public RestResponse GetCurrentHttpResponseData()
    {
        return GetCurrentHttpResponse();
    }
    public RestResponse GetCurrentHttpResponse()
    {
        return ContextDataHelper.Get<RestResponse>(
            ContextDataKeys.HttpCurrentResponse,
            _featureContext,
            _scenarioContext);
    }


}

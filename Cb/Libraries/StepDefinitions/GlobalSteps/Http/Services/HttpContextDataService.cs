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
}

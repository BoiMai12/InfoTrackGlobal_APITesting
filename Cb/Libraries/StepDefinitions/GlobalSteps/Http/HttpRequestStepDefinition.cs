using Libraries.StepDefinitions.GlobalSteps.Context.Services;
using Libraries.StepDefinitions.GlobalSteps.Http.Model;
using Libraries.StepDefinitions.GlobalSteps.Http.Services;
using Libraries.Support.Http;
using Libraries.Support.TestData;
using Microsoft.Extensions.Configuration;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.StepDefinitions.GlobalSteps.Http;

[Binding]
public class HttpRequestStepDefinition
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextDataService _httpContextDataService;
    private readonly IHttpService _httpService;
    private readonly ScenarioContext _scenarioContext;
    private readonly FeatureContext _featureContext;
    private readonly IVariableContextDataService _variableContextDataService;
    private readonly HttpResponseStepDefinition _httpResponseStepDefinition;

    //Use ThreadLocal<Dictionary<string> ensure thread safety when running parallel tests
    private static ThreadLocal<Dictionary<string, string>> _replacedPlaceholders =
        new(() => new Dictionary<string, string>());

    public HttpRequestStepDefinition(
        IHttpService httpService,
        IConfiguration configuration,
        IHttpContextDataService httpContextDataService,
        IVariableContextDataService variableContextDataService,
        ScenarioContext scenarioContext,
        FeatureContext featureContext,
        HttpResponseStepDefinition httpResponseStepDefinition)
    {
        _httpService = httpService;
        _configuration = configuration;
        _httpContextDataService = httpContextDataService;
        _variableContextDataService = variableContextDataService;
        _scenarioContext = scenarioContext;
        _featureContext = featureContext;
        _httpResponseStepDefinition = httpResponseStepDefinition;
    }

    #region Given
    [Given(@"I set HTTP base URL from appSettings ""([^""]*)""")]
    public void GivenISetHTTPBaseURLFromAppSettings(string key)
    {
        var data = new HttpBaseRequestContextData();
        data.BaseUrl = _configuration[key];
        _httpContextDataService.AddHttpBaseData(data);
    }

    [Given(@"I set the request headers:")]
    public void GivenISetTheRequestHeaders(DataTable dataTable)
    {
        var data = new HttpRequestContextData
        {
            QueryHeader = new List<HttpRequestHeader>()
        };
        foreach (var row in dataTable.Rows)
        {
            var key = row["Key"];
            var value = row["Value"];
            data.QueryHeader.Add(new HttpRequestHeader(key, value));
        }
        _httpContextDataService.AddCurrentHttpData(data);
    }

    [Given(@"I add Http request content from the file: '([^']*)'")]
    public void GivenIAddHttpRequestContentFromTheFile(string path)
    {
        var data = new HttpBaseRequestContextData
        {
            Content = TestDataHelper.ReadFile(path)
        };
        _httpContextDataService.AddCurrentHttpData(data);
    }


    #endregion



}


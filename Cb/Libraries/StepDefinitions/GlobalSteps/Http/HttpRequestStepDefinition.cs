using Libraries.StepDefinitions.GlobalSteps.Context.Services;
using Libraries.StepDefinitions.GlobalSteps.Http.Model;
using Libraries.StepDefinitions.GlobalSteps.Http.Services;
using Libraries.Support.Http;
using Libraries.Support.TestData;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Reqnroll;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
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

    [Given(@"I add Http request content from the file: '([^']*)'")]
    public void GivenIAddHttpRequestContentFromTheFile(string path)
    {
        var apiKey = _configuration["Staging:x-api-key"];


        var data = new HttpRequestContextData
        {
            QueryHeader = new List<HttpRequestHeader>(),
            Content = TestDataHelper.ReadFile(path)
        };
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            data.QueryHeader ??= new List<HttpRequestHeader>();
            data.QueryHeader.Add(new HttpRequestHeader("x-api-key", apiKey));
        }
        _httpContextDataService.AddCurrentHttpData(data);
    }

    #endregion

    [When(@"I send a POST request to '([^']*)'")]
    public void WhenISendAPOSTRequestTo(string url)
    {

        var httpBaseRequest = _httpContextDataService.GetHttpBaseData();
        var currentHttpRequest = _httpContextDataService.GetCurrentHttpData();
        var response = _httpService.Post(httpBaseRequest.BaseUrl, url, DataFormat.Json,
            currentHttpRequest.Content,currentHttpRequest.QueryHeader);
        _httpContextDataService.AddCurrentHttpResponseData(response);
    }




}


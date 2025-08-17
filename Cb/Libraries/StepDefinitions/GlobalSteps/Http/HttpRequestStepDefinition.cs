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
        var bearerToken = _variableContextDataService.GetVariable("BearerToken");
        var data = new HttpRequestContextData
        {
            QueryHeader = new List<HttpRequestHeader>(),
            Content = TestDataHelper.ReadFile(path)
        };
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            data.QueryHeader ??= new List<HttpRequestHeader>();
            data.QueryHeader.Add(new HttpRequestHeader("x-api-key", apiKey!));
        }

        if (!string.IsNullOrWhiteSpace(bearerToken))
        {
            data.QueryHeader ??= new List<HttpRequestHeader>();
            data.QueryHeader.Add(new HttpRequestHeader("Authorization", $"Bearer {bearerToken!}"));
        }

        _httpContextDataService.AddCurrentHttpData(data);
    }

    [Given("I add Http request content from the file: '(.*)' with data from appSettings")]
    public void GivenIAddHttpRequestContentFromTheFileWithDataFromAppSettings(string path)
    {
        var email = _configuration["Staging:email"];
        var password = _configuration["Staging:password"];
        var apiKey = _configuration["Staging:x-api-key"];
        var jsonContentTemplate = TestDataHelper.ReadFile(path);
        if (jsonContentTemplate.Contains("emailPlaceHolder"))
            jsonContentTemplate = jsonContentTemplate.Replace("emailPlaceHolder", email);
        if (jsonContentTemplate.Contains("passwordPlaceHolder"))
            jsonContentTemplate = jsonContentTemplate.Replace("passwordPlaceHolder", password);

        var data = new HttpRequestContextData
        {
            QueryHeader = new List<HttpRequestHeader>(),
            Content = jsonContentTemplate
        };
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            data.QueryHeader ??= new List<HttpRequestHeader>();
            data.QueryHeader.Add(new HttpRequestHeader("x-api-key", apiKey));
        }
        _httpContextDataService.AddCurrentHttpData(data);
    }


    [Given(@"I add Http request content from the file: '(.*)' with invalid '(.*)'")]
    public void GivenIAddHttpRequestContentFromTheFileWithInvalid(string path, string auth)
    {
        var bearerToken = _variableContextDataService.GetVariable("BearerToken");
        var data = new HttpRequestContextData
        {
            QueryHeader = new List<HttpRequestHeader>(),
            Content = TestDataHelper.ReadFile(path)
        };
        data.QueryHeader ??= new List<HttpRequestHeader>();
        data.QueryHeader.Add(new HttpRequestHeader("x-api-key", auth));
        if (!string.IsNullOrWhiteSpace(bearerToken))
        {
            data.QueryHeader ??= new List<HttpRequestHeader>();
            data.QueryHeader.Add(new HttpRequestHeader("Authorization", $"Bearer {bearerToken}"));
        }
        _httpContextDataService.AddCurrentHttpData(data);
    }


    [Given(@"I send a GET request to '([^']*)'")]
    public async Task GivenISendAGETRequestTo(string url)
    {
        //var path = $"/api/users/{userId}";
        var apiKey = _configuration["Staging:x-api-key"];
        var bearerToken = _variableContextDataService.GetVariable("BearerToken");
        var httpBaseRequest = _httpContextDataService.GetHttpBaseData();
        var data = new HttpRequestContextData
        {
            QueryHeader = new List<HttpRequestHeader>(),
        };
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            data.QueryHeader ??= new List<HttpRequestHeader>();
            data.QueryHeader.Add(new HttpRequestHeader("x-api-key", apiKey));
        }
        if (!string.IsNullOrWhiteSpace(bearerToken))
        {
            data.QueryHeader ??= new List<HttpRequestHeader>();
            data.QueryHeader.Add(new HttpRequestHeader("Authorization", $"Bearer {bearerToken}"));
        }
        _httpContextDataService.AddCurrentHttpData(data);

        var currentHttpRequest = _httpContextDataService.GetCurrentHttpData();
        var response = await _httpService.Get(httpBaseRequest.BaseUrl, url,
            currentHttpRequest.QueryHeader);
        _httpContextDataService.AddCurrentHttpResponse(response);
    }

    [Given(@"I send a Delete to '([^']*)'")]
    public async Task GivenISendADeleteTo(string url)
    {
        var apiKey = _configuration["Staging:x-api-key"];
        var bearerToken = _variableContextDataService.GetVariable("BearerToken");
        var httpBaseRequest = _httpContextDataService.GetHttpBaseData();
        var data = new HttpRequestContextData
        {
            QueryHeader = new List<HttpRequestHeader>(),
        };
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            data.QueryHeader ??= new List<HttpRequestHeader>();
            data.QueryHeader.Add(new HttpRequestHeader("x-api-key", apiKey));
        }
        if (!string.IsNullOrWhiteSpace(bearerToken))
        {
            data.QueryHeader ??= new List<HttpRequestHeader>();
            data.QueryHeader.Add(new HttpRequestHeader("Authorization", $"Bearer {bearerToken}"));
        }
        _httpContextDataService.AddCurrentHttpData(data);

        var currentHttpRequest = _httpContextDataService.GetCurrentHttpData();
        var response = await _httpService.Delete(httpBaseRequest.BaseUrl, url,
            currentHttpRequest.QueryHeader);
        _httpContextDataService.AddCurrentHttpResponse(response);
    }


    [Given(@"I pass invalid authentication '(.*)' into header")]
    public void GivenIPassInvalidAuthenticationIntoHeader(string auth)
    {
        var bearerToken = _variableContextDataService.GetVariable("BearerToken");
        var data = new HttpRequestContextData
        {
            QueryHeader = new List<HttpRequestHeader>(),
        };
        if (!string.IsNullOrWhiteSpace(auth))
        {
            data.QueryHeader ??= new List<HttpRequestHeader>();
            data.QueryHeader.Add(new HttpRequestHeader("x-api-key", auth));
        }
        if (!string.IsNullOrWhiteSpace(bearerToken))
        {
            data.QueryHeader ??= new List<HttpRequestHeader>();
            data.QueryHeader.Add(new HttpRequestHeader("Authorization", $"Bearer {bearerToken}"));
        }
        _httpContextDataService.AddCurrentHttpData(data);
    }


    //[Given("I send Get to '(.*)' with invalid authentication '(.*)'")]
    //public async Task GivenISendGetToWithInvalidAuthentication(string url, string auth)
    //{
    //    var bearerToken = _variableContextDataService.GetVariable("BearerToken");
    //    var httpBaseRequest = _httpContextDataService.GetHttpBaseData();
    //    var data = new HttpRequestContextData
    //    {
    //        QueryHeader = new List<HttpRequestHeader>(),
    //    };
    //    data.QueryHeader ??= new List<HttpRequestHeader>();
    //    data.QueryHeader.Add(new HttpRequestHeader("x-api-key", auth));
    //    if (!string.IsNullOrWhiteSpace(bearerToken))
    //    {
    //        data.QueryHeader ??= new List<HttpRequestHeader>();
    //        data.QueryHeader.Add(new HttpRequestHeader("Authorization", $"Bearer {bearerToken!}"));
    //    }
    //    _httpContextDataService.AddCurrentHttpData(data);

    //    var currentHttpRequest = _httpContextDataService.GetCurrentHttpData();
    //    var response = await _httpService.Get(httpBaseRequest.BaseUrl, url,
    //   currentHttpRequest.QueryHeader);
    //    _httpContextDataService.AddCurrentHttpResponse(response);
    //}



    #endregion

    [When(@"I send a POST request to '([^']*)'")]
    public async Task WhenISendAPOSTRequestTo(string url)
    {
        var httpBaseRequest = _httpContextDataService.GetHttpBaseData();
        var currentHttpRequest = _httpContextDataService.GetCurrentHttpData();
        var response = await _httpService.Post(httpBaseRequest.BaseUrl, url, DataFormat.Json,
            currentHttpRequest.Content, currentHttpRequest.QueryHeader);
        _httpContextDataService.AddCurrentHttpResponse(response);
    }

    [When(@"I send a PUT request to '([^']*)'")]
    public async Task WhenISendAPUTRequestTo(string url)
    {
        var httpBaseRequest = _httpContextDataService.GetHttpBaseData();
        var currentHttpRequest = _httpContextDataService.GetCurrentHttpData();
        var response = await _httpService.Put(httpBaseRequest.BaseUrl, url, DataFormat.Json,
            currentHttpRequest.Content, currentHttpRequest.QueryHeader);
        _httpContextDataService.AddCurrentHttpResponse(response);
    }

    [When(@"I send Get to '([^']*)'")]
    public async Task WhenISendGetTo(string url)
    {
        var httpBaseRequest = _httpContextDataService.GetHttpBaseData();
        var currentHttpRequest = _httpContextDataService.GetCurrentHttpData();
        var response = await _httpService.Get(httpBaseRequest.BaseUrl, url, currentHttpRequest.QueryHeader);
        _httpContextDataService.AddCurrentHttpResponse(response);
    }

    [Given(@"I send Delete to '([^']*)'")]
    [When(@"I send Delete to '([^']*)'")]
    public async Task WhenISendDeleteTo(string url)
    {
        var httpBaseRequest = _httpContextDataService.GetHttpBaseData();
        var currentHttpRequest = _httpContextDataService.GetCurrentHttpData();
        var response = await _httpService.Delete(httpBaseRequest.BaseUrl, url, currentHttpRequest.QueryHeader);
        _httpContextDataService.AddCurrentHttpResponse(response);
    }









}


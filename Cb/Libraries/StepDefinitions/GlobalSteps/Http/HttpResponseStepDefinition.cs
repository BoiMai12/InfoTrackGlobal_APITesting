using FluentAssertions;
using Libraries.StepDefinitions.GlobalSteps.Context.Services;
using Libraries.StepDefinitions.GlobalSteps.Http.Services;
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
public class HttpResponseStepDefinition
{
    private readonly IHttpContextDataService _httpContextDataService;
    private readonly ScenarioContext _scenarioContext;
    private readonly FeatureContext _featureContext;
    private readonly IVariableContextDataService _variableContextDataService;
  

    public HttpResponseStepDefinition(
        IHttpContextDataService httpContextDataService,
        IVariableContextDataService variableContextDataService,
        ScenarioContext context,
        FeatureContext featureContext)

    {
        _httpContextDataService = httpContextDataService;
        _variableContextDataService = variableContextDataService;
        _scenarioContext = context;
        _featureContext = featureContext;
    }

    #region Then

    [Then(@"I should receive the HTTP response status code '([^']*)'")]
    public void ThenIShouldReceiveTheHTTPResponseStatusCode(int expected)
    {
        var response = _httpContextDataService.GetCurrentHttpResponseData();
        ((int)response.StatusCode).Should().Be(expected);
    }

    [Then("I should receive the HTTP response has '(.+)' is '(.+)'")]
    public void ThenIShouldReceiveTheHTTPResponseHasIs(string path, string expected)
    {
        var response = _httpContextDataService.GetCurrentHttpResponseData();
        var selectdValue = GetSelectedJsonPathAsString(response, path);
        selectdValue.Should().Be(expected);
    }

    [Then("I should receive the HTTP response has '(.+)' is not NULL")]
    public void ThenIShouldReceiveTheHTTPResponseHasIsNotNULL(string path)
    {
        var response = _httpContextDataService.GetCurrentHttpResponseData();
        var selectdValue = GetSelectedJsonPathAsString(response, path);
        selectdValue.Should().NotBeNull();
    }

    #endregion

    #region Support
    public string GetSelectedJsonPathAsString(RestResponse response, string path)
    {
        var selectedPath = GetSelectedJsonPathAsObject(response, path)?? throw new NullReferenceException();
        return selectedPath.Value<string>();

    }

    private JToken GetSelectedJsonPathAsObject(RestResponse response, string path)
    {
        JToken jsonObject = JObject.Parse(response.Content ?? throw new InvalidOperationException());
        var selectedToken = jsonObject.SelectToken(path);
        return selectedToken;
    }

    #endregion

}


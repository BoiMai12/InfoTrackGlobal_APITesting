using Libraries.StepDefinitions.GlobalSteps.Context.Services;
using Libraries.StepDefinitions.GlobalSteps.Http.Services;
using Reqnroll;
using System;
using System.Collections.Generic;
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




}


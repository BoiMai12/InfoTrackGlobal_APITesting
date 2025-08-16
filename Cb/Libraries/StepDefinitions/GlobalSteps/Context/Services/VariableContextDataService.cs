using Libraries.Support.Context;
using Libraries.Support.Constrains;
using Reqnroll;


namespace Libraries.StepDefinitions.GlobalSteps.Context.Services;

    public class VariableContextDataService : IVariableContextDataService
    {
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;
        public VariableContextDataService(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
        _featureContext = featureContext;
        _scenarioContext = scenarioContext;
        }

        public void AddVariable(string variableName, string? value) 
        {
        _scenarioContext.Add(GetContextKey(variableName), value);
        _featureContext.Add(GetContextKey(variableName), value);
    }
        public string GetVariable(string variableName)
        {
            return ContextDataHelper.Get<string>(
            GetContextKey(variableName),
            _featureContext,
            _scenarioContext);
        }
        private string GetContextKey(string variableName)
        {
            return $"{ContextDataKeys.VariablePrefix}{variableName}";
        }
}


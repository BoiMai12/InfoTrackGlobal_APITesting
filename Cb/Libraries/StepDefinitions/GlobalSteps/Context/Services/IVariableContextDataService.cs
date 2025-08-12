namespace Libraries.StepDefinitions.GlobalSteps.Context.Services;

    public interface IVariableContextDataService
    {
       public void AddVariable(String variableName, string? value);
       public string GetVariable (String variableName);

    }


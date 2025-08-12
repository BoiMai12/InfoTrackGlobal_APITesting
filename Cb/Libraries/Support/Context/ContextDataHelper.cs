using CucumberExpressions.Parsing;
using Reqnroll;

namespace Libraries.Support.Context;

    public abstract class ContextDataHelper
    {
        public static TResult Get<TResult>(string key, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            if (scenarioContext.TryGetValue<TResult>(key, out var output)) return output;

            return featureContext.Get<TResult>(key);
        }
        public static void Add<TResult>(string key, TResult data, FeatureContext featureContext)
        {
            featureContext.Add(key, data);
        }

        public static void Add<TResult>(string key, TResult data, ScenarioContext scenarioContext)
        {
            scenarioContext.Add(key, data);
        }
}


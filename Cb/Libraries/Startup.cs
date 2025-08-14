using Libraries.StepDefinitions.GlobalSteps.Context.Services;
using Libraries.StepDefinitions.GlobalSteps.Http;
using Libraries.StepDefinitions.GlobalSteps.Http.Services;
using Libraries.Support.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace Libraries;

public static class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        services.AddScoped<IHttpService,HttpService>();
        services.AddScoped<IHttpContextDataService, HttpContextDataService>();
        services.AddScoped<IVariableContextDataService, VariableContextDataService>();
        services.AddSingleton(_ => BuildConfiguration());
        services.AddScoped<HttpRequestStepDefinition>();
        services.AddScoped<HttpResponseStepDefinition>();

        return services;
    }

    public static IConfiguration BuildConfiguration()
    {
        var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appSettings.json", true, true);
        if (!string.IsNullOrEmpty(environmentName))
        {
            builder.AddJsonFile($"appSettings.{environmentName}.json", true, true);
        }
        builder.AddEnvironmentVariables();
        return builder.Build();
    }

}


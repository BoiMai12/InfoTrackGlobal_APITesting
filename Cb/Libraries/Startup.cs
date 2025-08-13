using Libraries.StepDefinitions.GlobalSteps.Context.Services;
using Libraries.StepDefinitions.GlobalSteps.Http.Services;
using Libraries.Support.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries;

public abstract class Startup
{
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        services.AddScoped<IHttpService,HttpService>();
        services.AddScoped<IHttpContextDataService, HttpContextDataService>();
        services.AddScoped<IVariableContextDataService, VariableContextDataService>();
        services.AddSingleton(_ => BuildConfiguration());
        //services.AddScoped<HttpRequestStepDefinition>();
        //services.AddScoped<HttpResponseStepDefinition>();

        return services;
    }

    public static IConfiguration BuildConfiguration()
    {
        var environmentName = Environment.GetEnvironmentVariable("");
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


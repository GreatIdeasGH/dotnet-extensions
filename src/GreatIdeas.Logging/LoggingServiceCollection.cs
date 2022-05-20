using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace GreatIdeas.Logging;

public static class LoggingServiceCollection
{
    /// <summary>
    /// Adds the logging services using Serilog and persisting to Seq, Elasticsearch and Application Insights. By default logging services are not enabled.
    /// </summary>
    /// <param name="useSeq">Enable Seq</param>
    /// <param name="useElasticSearch">Enable Elastic Search</param>
    /// <param name="useApplicationInsights">Enable Application Insights</param>
    public static void AddLoggingServices(this WebApplicationBuilder builder, bool? useSeq = null, bool? useElasticSearch = null, bool? useApplicationInsights = null)
    {
        Log.Information("Starting web host ...");

        // Get assembly information
        var currentAssembly = Assembly.GetEntryAssembly();
        var assemblyName = currentAssembly!.GetName().Name?.ToLower().Replace(".", "-");
        var loggingName = $"{assemblyName}-{builder.Environment.EnvironmentName?.ToLower().Substring(0, 3)}-logs-";

        // Full setup of serilog logging
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            // .MinimumLevel.Information()
            configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}");

            configuration.WriteTo.File($"logs/{loggingName}.log", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: null);

            if (useSeq.HasValue && useSeq.Value)
            {
                configuration.WriteTo.Seq(builder.Configuration["SeqConfiguration:Uri"]);
            }

            if (useElasticSearch.HasValue && useElasticSearch!.Value)
            {
                configuration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration["ElasticConfiguration:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    NumberOfShards = 2,
                    IndexFormat =
                    $"{loggingName}{DateTime.UtcNow:yyyy-MM-dd}"
                });
            }

            if (useApplicationInsights.HasValue && useApplicationInsights!.Value)
            {
                configuration.WriteTo.ApplicationInsights(builder.Services.BuildServiceProvider().GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces, LogEventLevel.Information);
                configuration.WriteTo.ApplicationInsights(builder.Services.BuildServiceProvider().GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Events, LogEventLevel.Information);
            }

        });


        if (useApplicationInsights!.Value)
        {
            // Application Insights
            builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);
        }
    }


    /// <summary>
    /// Add Serilog boostrap to the beginning of Program.cs to call this method before the application runs.
    /// </summary>
    public static void AddSerilogBootstrapLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .WriteTo.Console()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .CreateBootstrapLogger();

        Log.Information("Starting up ...");
    }

}

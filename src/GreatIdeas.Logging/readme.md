# GreatIdeas.Logging

This package simplifies the integration of Serilog with persistance stores such as Seq, Elastic Search and Kibana, and Application Insights.

## Serilog Settings
The general serilog configuration is utilized here in the `appsettings.json` as indicated below:
```json
{
 "Serilog": {
	"MinimumLevel": {
	  "Default": "Information",
	  "Override": {
	    "Microsoft": "Information",
	    "Microsoft.AspNetCore.Hosting": "Information",
	    "System": "Warning"
	   }
	}
 },
 "AllowedHosts": "*"
}
```

### Seq Configuration
The following json configuration is required to Seq configuration:
```json
"SeqConfiguration": {
   "Uri": "http://localhost:5341"
}
```

### Elastic Search Configuration
Elastic search and Kibana are both required to effectively logging and UI capabilities. Provide the following for elastic endpoint: 
```json
"ElasticConfiguration": {
   "Uri": "http://localhost:9200"
}
```

### Application Insights Configuration
With Application Insights, you will need to possess the  Instrumentation key for the resource created in Azure. Also provide the following configuration in `appsettings.json`.
```json
"ApplicationInsights": {
  "ConnectionString": "<Copy connection string from Application Insights Resource Overview>"
}
```

## Dependency Injection
After successfully setting up the configurations in `appsettings` or other secret managers, use the following to enable the Serilog integration with the above stores.
```csharp
using GreatIdeas.Logging;
using Serilog;
using Serilog.Events;

// Add Serilog boostrap to the beginning of program.cs  
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
	.Enrich.FromLogContext()
	.Enrich.WithMachineName()
	.CreateBootstrapLogger();

try
{
	var builder = WebApplication.CreateBuilder(args);  

	// Add logging
	builder.AddLoggingServices(options => 
	{
		 options.UseApplicationInsights = true;
		 options.UseSeq = false;
		 options.UseElasticSearch = false;
	});
	
	// Other code removed for brevity

	var app = builder.Build();

	// Serilog Middleware
	app.UseSerilogRequestLogging(config =>	
	{
		config.MessageTemplate = 
			 "HTTP {RequestMethod} {RequestPath} {UserId} responded {StatusCode} in {Elapsed:0.0000} ms";	
	 });	

	// Other code below ...

	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Fatal error occurred");
	return 1;
}
finally
{
	Log.CloseAndFlush();
}

return 0;
```

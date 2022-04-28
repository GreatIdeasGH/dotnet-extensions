using Microsoft.AspNetCore.Builder;
using Serilog;

namespace GreatIdeas.Logging;

public static class LoggingServiceMiddleware
{
    public static void UseSerilogCustomLoggingMiddleware(this WebApplication app)
    {
        app.UseSerilogRequestLogging(config =>
        {
            config.MessageTemplate =
                "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
        });
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace GreatIdeas.MailServices.MsGraph;

public static class MailServiceCollection
{
    /// <summary>
    /// Add DI for <see cref="MsGraphService"/> with configuration
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddMsGraphMailService(this IServiceCollection services, IConfiguration configuration)
    {
        // Azure Storage Settings
        services.Configure<AzureAdOptions>(configuration.GetSection("AzureAd"));

        // Register MsGraphMailService
        services.AddTransient<IMsGraphService, MsGraphService>();

        services.AddMicrosoftIdentityWebApiAuthentication(configuration)
            .EnableTokenAcquisitionToCallDownstreamApi()
            .AddMicrosoftGraph(configuration.GetSection("DownstreamApi"))
            .AddInMemoryTokenCaches();

        var clientId = configuration["AzureAd:ClientId"];

        return services;
    }

}

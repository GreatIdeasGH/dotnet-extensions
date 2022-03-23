using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static GreatIdeas.MailServices.MsGraphService;

namespace GreatIdeas.MailServices
{
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

            return services;
        }

        /// <summary>
        /// Add DI for <see cref="SendGridService"/> with configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSendGridMailService(this IServiceCollection services, IConfiguration configuration)
        {
            // Azure Storage Settings
            services.Configure<SendGridService.SendGridOptions>(configuration.GetSection("SendGrid"));

            // Register SendGridService
            services.AddTransient<ISendGridService, SendGridService>();

            return services;
        }

    }
}

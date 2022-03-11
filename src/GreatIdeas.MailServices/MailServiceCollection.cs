using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreatIdeas.MailServices
{
    public static class MailServiceCollection
    {
        /// <summary>
        /// Add DI for <see cref="MsGraphMailService"/> with configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMsGraphMailService(this IServiceCollection services, IConfiguration configuration)
        {
            // Azure Storage Settings
            services.Configure<MsGraphMailService.AzureAdOptions>(configuration.GetSection("AzureAd"));

            // Register MsGraphMailService
            services.AddTransient<IMsGraphMailService, MsGraphMailService>();

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
            // Register SendGridService
            services.AddTransient<ISendGridService, SendGridService>();

            // Azure Storage Settings
            services.Configure<SendGridService.SendGridOptions>(configuration.GetSection("SendGrid"));

            return services;
        }

    }
}

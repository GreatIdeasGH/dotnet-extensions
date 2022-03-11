using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace GreatIdeas.MailServices;

public class MsGraphMailService : IMsGraphMailService
{
    /// <summary>
    /// Set the credentials of Azure AD
    /// </summary>
    /// <param name="TenantId"></param>
    /// <param name="ClientId"></param>
    /// <param name="ClientSecret"></param>
    /// <param name="UserObjectId">The user account ID of the user's emailModel address</param>
    public record AzureAdOptions(string TenantId, string ClientId, string ClientSecret, string UserObjectId);

    private readonly IOptionsMonitor<AzureAdOptions> _optionsMonitor;
    private readonly AzureAdOptions _azureAdOptions;

    public MsGraphMailService(IOptionsMonitor<AzureAdOptions> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
        _azureAdOptions = optionsMonitor.CurrentValue;
    }

    /// <summary>
    /// Send emailModel using Microsoft Graph API
    /// </summary>
    /// <param name="emailModel"><see cref="EmailModel"/></param>
    /// <returns><see cref="string"/> for success or failure</returns>
    public async Task<bool> SendEmailAsync(EmailModel emailModel)
    {
        try
        {
            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var clientSecretCredential = new ClientSecretCredential(
                _azureAdOptions.TenantId, _azureAdOptions.ClientId, _azureAdOptions.ClientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential);

            // Define a simple e-mail message.
            var message = new Message
            {
                Subject = emailModel.Subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = emailModel.Body
                },
                ToRecipients = new List<Recipient>()
                {
                    new Recipient {EmailAddress = new EmailAddress {Address = emailModel.To}}
                }
            };

            // Send mail as the given user. 
            await graphClient
                 .Users[_azureAdOptions.UserObjectId]
                 .SendMail(message, true)
                 .Request()
                 .PostAsync();

            return true;
        }
        catch (Exception e)
        {
            throw new Exception("Email delivery failed", e);
        }
    }
}
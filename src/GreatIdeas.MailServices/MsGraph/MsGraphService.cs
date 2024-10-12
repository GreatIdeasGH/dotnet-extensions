using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace GreatIdeas.MailServices.MsGraph;

public class MsGraphService : IMsGraphService
{
    private readonly AzureAdOptions _azureAdOptions;

    public MsGraphService(IOptionsMonitor<AzureAdOptions> optionsMonitor)
    {
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
                    new() {EmailAddress = new EmailAddress {Address = emailModel.To}}
                },
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

    /// <summary>
    /// Send emailModel using Microsoft Graph API
    /// </summary>
    /// <param name="emailModel"><see cref="EmailModel"/></param>
    /// <param name="fileToAttach"></param>
    /// <returns><see cref="string"/> for success or failure</returns>
    public async Task<bool> SendEmailWithAttachmentAsync(EmailModel emailModel, FileToAttach fileToAttach)
    {
        try
        {
            // Client Secret Credential
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            var clientSecretCredential = new ClientSecretCredential(
                _azureAdOptions.TenantId, _azureAdOptions.ClientId, _azureAdOptions.ClientSecret, options);
            var graphClient = new GraphServiceClient(clientSecretCredential);

            // Message
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
                    new() {EmailAddress = new EmailAddress {Address = emailModel.To}}
                },
                Attachments = new MessageAttachmentsCollectionPage()
                {
                    new FileAttachment
                    {
                        ODataType = "#microsoft.graph.fileAttachment",
                        ContentBytes = fileToAttach.Bytes,
                        Name = fileToAttach.FileName,
                        ContentType = "application/octet-stream",
                        Size =  fileToAttach.FileSize,
                    },
                }
            };

            // Send attachment with maximum size of 4MB
            if (fileToAttach.FileSize > 4 * 1024 * 1024)
            {
                throw new Exception("File size is greater than 4MB");
            }

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
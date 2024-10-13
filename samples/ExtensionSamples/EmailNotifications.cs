using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ExtensionSamples;

internal class EmailNotifications(ILogger<EmailNotifications> logger, IConfiguration configuration)
{
    public void SendNotification()
    {
        // Azure Comm Service configuration (ACS)
        string connectionString = configuration["ACS:ConnectionString"]!;
        string sender = configuration["ACS:Sender"]!;
        string recipient = configuration["ACS:Recipient"]!;

        var emailClient = new EmailClient(connectionString);

        var htmlContent = "<html><body><h1>Quick send email test</h1><br/><h4>Communication email as a service mail send app working properly</h4><p>Happy Learning!!</p></body></html>";

        try
        {
            var emailMessage = new EmailMessage(
           senderAddress: sender,
           content: new EmailContent("Test Email")
           {
               PlainText = "Hello world via email.",
               Html = htmlContent
           },
           recipients: new EmailRecipients(new List<EmailAddress> { new(recipient) }));

            EmailSendOperation emailSendOperation = emailClient.Send(WaitUntil.Completed, emailMessage);

            logger.LogInformation("Email sent. Status: {EmailStatus}", emailSendOperation.Value.Status);
        }
        catch (RequestFailedException ex)
        {
            /// OperationID is contained in the exception message and can be used for troubleshooting purposes
            logger.LogError($"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}");
        }
    }
}

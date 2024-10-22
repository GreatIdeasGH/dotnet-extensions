using System.Text.RegularExpressions;
using System.Web;
using Azure;
using Azure.Communication.Email;
using GreatIdeas.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ExtensionSamples;

public enum ExceptionNotifications
{
    CriticalErrorAlert,
    HighPriorityException,
    SystemMalfunctionNotice,
    UrgentBugNotification,
    ApplicationCrashAlert,
}

internal class EmailNotifications(ILogger<EmailNotifications> logger, IConfiguration configuration)
{
    public void SendNotification(ExceptionNotifications subject, Exception exception)
    {
        // Azure Comm Service configuration (ACS)
        string connectionString = configuration["ACS:ConnectionString"]!;
        string sender = configuration["ACS:Sender"]!;
        string recipient = configuration["ACS:Recipient"]!;

        var emailClient = new EmailClient(connectionString);

        var htmlContent = $"""
                <h2 style='color:red;'>Exception Notification 🐞</h2>
                <p><strong>Date:</strong> {TimeProvider.System.GetLocalNow()}</p>
                <p><strong>Platform:</strong> {Environment.OSVersion.Platform}</p>
                <p><strong>Environment:</strong> {Environment.GetEnvironmentVariable(
                    "ASPNETCORE_ENVIRONMENT"
                ) ?? "Production"}</p>
                <p><strong>OS:</strong> {Environment.OSVersion}</p>
                <p><strong>Machine Name:</strong> {Environment.MachineName}</p>
                <p><strong>Error Message:</strong></p>
                <code>{HttpUtility.HtmlEncode(exception.Message)}</code>
                <p><strong>Detailed Message:</strong></p>
                <code>{HttpUtility.HtmlEncode(exception.InnerException)}</code>
                <p>Please take the necessary actions to resolve the following issue.</p>
                <footer>
                    <p>Automated emails</p>
                </footer>
                """;

        try
        {
            var emailMessage = new EmailMessage(
                senderAddress: sender,
                content: new EmailContent(
                    $"{subject.ToString().SplitCamelCase()}: {TimeProvider.System.GetLocalNow()}"
                )
                {
                    Html = htmlContent,
                },
                recipients: new EmailRecipients(new List<EmailAddress> { new(recipient) })
            );

            EmailSendOperation emailSendOperation = emailClient.Send(
                WaitUntil.Completed,
                emailMessage
            );

            logger.LogInformation(
                "Email sent. Status: {EmailStatus}",
                emailSendOperation.Value.Status
            );
        }
        catch (RequestFailedException ex)
        {
            /// OperationID is contained in the exception message and can be used for troubleshooting purposes
            logger.LogError(
                $"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}"
            );
        }
    }
}

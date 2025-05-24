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

public class EmailNotifications(ILogger<EmailNotifications> logger, IConfiguration configuration)
{
    /// <summary>
    /// Sends an email notification containing detailed exception information using Azure Communication Services.
    /// </summary>
    /// <param name="subject">The type of exception notification to be sent.</param>
    /// <param name="exception">The exception details to be included in the notification.</param>
    /// <remarks>
    /// The email includes system information such as:
    /// - Current date and time
    /// - Platform details
    /// - Environment information
    /// - Operating system details
    /// - Machine name
    /// - Exception message and inner exception details
    /// Configuration requirements in appsettings.json:
    /// <code>
    /// {
    ///   "ACS": {
    ///     "ConnectionString": "endpoint=https://your-acs.communication.azure.com/;accesskey=your-key",
    ///     "Sender": "DoNotReply@your-domain.com",
    ///     "Recipient": "admin@your-domain.com"
    ///   }
    /// }
    /// </code>
    /// </remarks>
    /// <exception cref="RequestFailedException">
    /// Thrown when the email sending operation fails through Azure Communication Services.
    /// </exception>
    public async ValueTask Send(ExceptionNotifications subject, Exception exception)
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

            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
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
            logger.LogCritical(ex, "Email send operation failed.");
        }
    }
}

using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GreatIdeas.MailServices.SendGrid;

public class SendGridService : ISendGridService
{
    public class SendGridOptions
    {
        public string? ApiKey { get; set; }
    }

    private readonly SendGridOptions _sendGridOptions;

    public SendGridService(IOptionsMonitor<SendGridOptions> optionsMonitor)
    {
        _sendGridOptions = optionsMonitor.CurrentValue;
    }


    /// <summary>
    /// Send emailModel using SendGrid API
    /// </summary>
    /// <param name="emailModel"><see cref="EmailModel"/></param>
    /// <returns>SendGrid <see cref="Response"/></returns>
    public async Task<Response> SendEmailAsync(EmailModel emailModel)
    {
        var response = await Execute(emailModel);
        return response;
    }

    private async Task<Response> Execute(EmailModel emailModel)
    {
        var client = new SendGridClient(_sendGridOptions.ApiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(emailModel.FromAddress),
            Subject = emailModel.Subject,
            PlainTextContent = emailModel.Body,
            HtmlContent = emailModel.Body
        };
        msg.AddTo(new EmailAddress(emailModel.To, emailModel.FromName));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);

        var response = await client.SendEmailAsync(msg);

        return response;
    }
}

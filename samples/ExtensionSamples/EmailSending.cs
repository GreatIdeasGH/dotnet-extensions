using GreatIdeas.MailServices;
using GreatIdeas.MailServices.MsGraph;
using Microsoft.Extensions.Logging;

internal class EmailSending(
    IMsGraphService msGraphService,
    ILogger<EmailSending> logger
)
{
    public async ValueTask SendEmailWithGraph()
    {
        var emailModel = new EmailModel
        {
            FromAddress = "admin@email.com",
            To = "geraldmaale@hotmail.com",
            FromName = "Jude",
            Body = "Hello there",
            Subject = "Greetings!",
        };
        var result = await msGraphService.SendEmailAsync(emailModel);

        logger.LogInformation("Sending email:{Result}", result);
    }
}

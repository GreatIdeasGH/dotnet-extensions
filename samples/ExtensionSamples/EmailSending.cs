using GreatIdeas.MailServices;
using GreatIdeas.MailServices.MsGraph;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

internal class EmailSending(
    IMsGraphService msGraphService,
    ILogger<EmailSending> logger,
    IConfiguration configuration
)
{
    private readonly string _sender = configuration[""]!;
    private readonly string _recipient = configuration["MsGraphRecipientAddress"]!;

    public async ValueTask SendEmailWithGraph()
    {
        var emailModel = new EmailModel(_recipient, "Hello there", "Greetings!");

        var result = await msGraphService.SendEmailAsync(emailModel);

        logger.LogInformation("Sending email:{Result}", result);
    }
}

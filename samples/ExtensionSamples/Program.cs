using GreatIdeas.MailServices;
using GreatIdeas.MailServices.MsGraph;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddLogging(l => l.AddConsole());
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddMsGraphMailService(builder.Configuration);
builder.Services.AddScoped<SampleEmailSending>();

var host = builder.Build();

var sendingService = host.Services.GetRequiredService<SampleEmailSending>();
await sendingService.SendEmail();

host.Start();


internal class SampleEmailSending(IMsGraphService msGraphService, ILogger<SampleEmailSending> logger)
{
    public async ValueTask SendEmail()
    {
        var emailModel = new EmailModel { FromAddress = "admin@ggcedutech.com", To = "geraldmaale@hotmail.com", FromName = "Jude", Body = "Hello there", Subject = "Greetings!" };
        var result = await msGraphService.SendEmailAsync(emailModel);

        logger.LogInformation("Sending email:{result}", result);
    }
}
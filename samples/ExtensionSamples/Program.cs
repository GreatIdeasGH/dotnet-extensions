using ExtensionSamples;
using GreatIdeas.MailServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddLogging(l => l.AddConsole());
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddMsGraphMailService(builder.Configuration);
builder.Services.AddScoped<EmailSending>();
builder.Services.AddScoped<EmailNotifications>();

var host = builder.Build();

var sendingService = host.Services.GetRequiredService<EmailSending>();
var notificationService = host.Services.GetRequiredService<EmailNotifications>();

// Send email as a user
// await sendingService.SendEmailWithGraph();

// Send notification to a user
try
{
    throw new ArgumentNullException("");
}
catch (Exception exception)
{
    notificationService.Send(ExceptionNotifications.UrgentBugNotification, exception);
}

host.Start();

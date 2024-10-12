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

var host = builder.Build();

var sendingService = host.Services.GetRequiredService<EmailSending>();
await sendingService.SendEmailWithGraph();

host.Start();

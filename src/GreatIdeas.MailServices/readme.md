# GreatIdeas MailServices

Simplifying sending of emails using the Microsoft Graph SDK and SendGrid in .NET.

## Getting Started

This repository implements the sending of emails using the Microsoft Graph SDK. You will need your Azure Active Directory details configured in the `appsettings.json` file.

### Microsoft Graph

For the Microsoft Graph SDK, you need to create an Azure AD application and grant the application the permissions to send emails on behalf of the user. You can find more information about this [link](https://learn.microsoft.com/en-us/graph/sdks/create-client?from=snippets&tabs=csharp).

MS Graph Authentication provider

+ https://learn.microsoft.com/en-us/graph/sdks/choose-authentication-providers?tabs=csharp


Add the following to the `appsettings.json` file.

NB: The `UserObjectId` is the `Object Id` of the user in Azure AD. This can be found in the Azure Portal under `Azure Active Directory` -> `Users` -> `User` -> `Overview` -> `Object ID`

```json
{
  "AzureAd": {
    "ClientId": "",
    "ClientSecret": "",
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "domain.com",
    "TenantId": "",
    "UserObjectId": ""
  },
  "AllowedHosts": "*"
}
```

Register the MSGraph service, which is registered as scoped.

```csharp

builder.Services.AddMsGraphMailService();

```

After registering `AddMsGraphMailService` in DI, the `IMsGraphMailService` is a convenient interface to use for sending the mails. 

Sample usage of MsGraph:

```csharp

using GreatIdeas.MailServices.MsGraph;

public interface IEmailSender
{
    Value<bool> SendEmailAsync(EmailModel model);
}

internal sealed class EmailSender(IMsGraphService msGraphService): IEmailSender
{    
    public async Value<bool> SendEmailAsync(EmailModel model)
    {
        bool response = await _msGraphService.SendEmailAsync(model);

        // rest of code
    }
}
```

### SendGrid

For SendGrid, you need to create an API key. You can find more information about this [here](https://docs.sendgrid.com/for-developers/sending-email/email-api-quickstart-for-c).

Register the SendGrid service, which is registered as scoped.

```csharp

builder.Services.AddSendGridMailService(configuration); // configuration -> IConfiguration

```

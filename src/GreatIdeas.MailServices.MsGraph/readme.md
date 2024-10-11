# GreatIdeas.MailServices.MsGraph

This repository implements the sending of emails using the Microsoft Graph SDK. You will need your Azure Active Directory details configured in the `appsettings.json` file.

The service is registered as scoped and can be injected into your application.

## Register the IMsGraphMailService

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

Register the service in the `program.cs` file

```csharp

builder.Services.AddMsGraphMailService();

```

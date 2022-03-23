namespace GreatIdeas.MailServices;

public interface IMsGraphService
{
    /// <summary>
    /// Send emailModel using Microsoft Graph API
    /// </summary>
    /// <param name="emailModel"><see cref="EmailModel"/></param>
    /// <returns><see cref="string"/> for success or failure</returns>
    Task<bool> SendEmailAsync(EmailModel emailModel);
    Task<bool> SendEmailWithAttachmentAsync(EmailModel emailModel, FileToAttach fileToAttach);
}
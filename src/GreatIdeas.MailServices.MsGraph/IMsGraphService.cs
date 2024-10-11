namespace GreatIdeas.MailServices.MsGraph;

public interface IMsGraphService
{
    /// <summary>
    /// Send emailModel using Microsoft Graph API
    /// </summary>
    /// <param name="emailModel"><see cref="EmailModel"/></param>
    /// <returns><see cref="string"/> for success or failure</returns>
    Task<bool> SendEmailAsync(EmailModel emailModel);

    /// <summary>
    /// Send emailModel using Microsoft Graph API with attachment
    /// </summary>
    /// <param name="emailModel"></param>
    /// <param name="fileToAttach"></param>
    /// <returns><see cref="string"/> for success or failure</returns>
    Task<bool> SendEmailWithAttachmentAsync(EmailModel emailModel, FileToAttach fileToAttach);
}
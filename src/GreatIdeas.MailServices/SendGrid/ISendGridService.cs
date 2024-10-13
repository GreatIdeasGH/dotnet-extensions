using SendGrid;

namespace GreatIdeas.MailServices.SendGrid;

public interface ISendGridService
{
    /// <summary>
    /// Send emailModel using SendGrid API
    /// </summary>
    /// <param name="emailModel"><see cref="SendGridEmailModel"/></param>
    /// <returns>SendGrid <see cref="Response"/></returns>
    Task<Response> SendEmailAsync(SendGridEmailModel emailModel);
}
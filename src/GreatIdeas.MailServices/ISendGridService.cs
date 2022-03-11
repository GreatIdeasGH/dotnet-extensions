using SendGrid;

namespace GreatIdeas.MailServices;

public interface ISendGridService
{
    /// <summary>
    /// Send emailModel using SendGrid API
    /// </summary>
    /// <param name="emailModel"><see cref="EmailModel"/></param>
    /// <returns>SendGrid <see cref="Response"/></returns>
    Task<Response> SendEmailAsync(EmailModel emailModel);
}
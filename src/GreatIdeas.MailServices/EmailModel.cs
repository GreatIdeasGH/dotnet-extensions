namespace GreatIdeas.MailServices;

public class EmailModel
{
    public string? To { get; set; }
    public string? FromAddress { get; set; }
    public string? FromName { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}
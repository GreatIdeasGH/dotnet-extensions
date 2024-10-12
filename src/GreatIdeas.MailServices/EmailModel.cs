namespace GreatIdeas.MailServices;

public sealed record EmailModel
{
    public required string To { get; set; }
    public required string FromAddress { get; set; }
    public string? FromName { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}
namespace GreatIdeas.MailServices;

public sealed record EmailModel(string To, string Subject, string Body);

public sealed record SendGridEmailModel(string To, string Subject, string Body, string FromName, string From);

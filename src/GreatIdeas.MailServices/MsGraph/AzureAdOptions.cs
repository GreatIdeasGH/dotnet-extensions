namespace GreatIdeas.MailServices.MsGraph;

public sealed record AzureAdOptions
{
    public required string TenantId { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string UserObjectId { get; set; }
}
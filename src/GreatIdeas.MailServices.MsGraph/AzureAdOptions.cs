namespace GreatIdeas.MailServices.MsGraph;

public record AzureAdOptions()
{
    public string TenantId { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public string UserObjectId { get; set; } = default!;
}
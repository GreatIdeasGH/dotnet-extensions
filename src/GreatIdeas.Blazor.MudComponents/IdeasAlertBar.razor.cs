using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GreatIdeas.Blazor.MudComponents;

public partial class IdeasAlertBar
{
    [Parameter]
    public string Content { get; set; } = string.Empty;

    [Parameter]
    public Severity Severity { get; set; } = Severity.Info;

    [Parameter]
    public bool Enable { get; set; }

    [Parameter] public string Class { get; set; } = "mb-2 mt-2";
}
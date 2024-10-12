using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GreatIdeas.Blazor.MudComponents;

public class IdeasCardBase : IdeasComponentBase
{
    ///<summary>Add buttons to card header</summary>
    [Parameter]
    public RenderFragment HeaderActions { get; set; } = default!;

    ///<summary>Class to apply css</summary>
    [Parameter]
    public Color Color { get; set; }

}
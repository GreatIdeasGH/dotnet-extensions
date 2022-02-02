using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace GreatIdeas.Blazor.MudComponents
{
    public partial class IdeasButton
    {
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public string Icon { get; set; }
        [Parameter]
        public string Caption { get; set; }

        [Parameter]
        public Color Color { get; set; } = Color.Default;

        [Parameter]
        public Variant Variant { get; set; } = Variant.Text;

        [Parameter]
        public string TooltipText { get; set; }

        [Parameter]
        public bool Disabled { get; set; }
    }
}
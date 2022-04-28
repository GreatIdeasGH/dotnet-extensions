using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GreatIdeas.Blazor.MudComponents
{
    public partial class IdeasAlertBar
    {
        [Parameter]
        public string Content { get; set; }

        [Parameter]
        public Severity Severity { get; set; } = Severity.Info;
        private string Theme()
        {
            return $"pa-2 mt-2 mud-theme-{Severity}";
        }

        [Parameter]
        public bool Enable { get; set; }

        [Parameter] public string Class { get; set; } = "mb-2 mt-2";
    }
}
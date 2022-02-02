using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GreatIdeas.Blazor.MudComponents
{
    public class IdeasDialogBase: ComponentBase
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string ContentText { get; set; }
        
        [Parameter]
        public RenderFragment Content { get; set; }

        [Parameter]
        public string ButtonText { get; set; }
        
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public Color Color { get; set; }

        public void Submit() => MudDialog.Close(DialogResult.Ok(true));
        public void Cancel() => MudDialog.Cancel();
        public void Cancel2() => IsVisible = false;
        
        ///<summary>Select Icon for display</summary>
        [Parameter]
        public string Icon { get; set; }

        ///<summary>Enable Icon</summary>
        [Parameter]
        public bool ShowIcon { get; set; }
        
        ///<summary>Enable Dialog</summary>
        [Parameter]
        public bool IsVisible { get; set; } = true;
    }
}
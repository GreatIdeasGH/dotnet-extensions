using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace GreatIdeas.Blazor.Dialog
{
    public partial class MessageDialog: ComponentBase
    {
        [Parameter]
        public string ConfirmationTitle { get; set; } = "Confirm Delete?";

        [Parameter]
        public string ConfirmationMessage { get; set; } = "Are you sure you want to delete the selected record?";
        
        [Parameter]
        public RenderFragment Template { get; set; }

        [Parameter] public bool ShowDeleteMessage { get; set; } = true;
        [Parameter] public bool ShowFooter { get; set; } = true;
        [Parameter] public string ActionButtonLabel { get; set; } = "Delete";

        
        protected bool ShowConfirmation { get; set; }
        
        public void ShowDialog()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }
    }
}

using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace GreatIdeas.Blazor.Dialog
{

    public partial class ModalDialog
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string Caption { get; set; }
        [Parameter] public RenderFragment Template { get; set; }
        [Parameter] public RenderFragment FooterTemplate { get; set; }
        [Parameter] public EventCallback<bool> ConfirmationChanged { get; set; }
        [Parameter] public string OkButtonLabel { get; set; } = "OK";

        [Parameter] public ModalDialogSize ModalSize { get; set; } = ModalDialogSize.Small;

        protected bool ShowConfirmation { get; set; }
        
        public void ShowDialog()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }
    }

    [JsonConverter(typeof (JsonStringEnumConverter))]
    public enum ModalDialogSize
    {
        [EnumMember(Value = "")] Default,
        [EnumMember(Value = "modal-sm")] Small,
        [EnumMember(Value = "modal-lg")] Large
    }
}
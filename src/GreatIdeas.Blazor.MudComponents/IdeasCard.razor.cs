using Microsoft.AspNetCore.Components;
using MudBlazor;
using GreatIdeas.Extensions;

namespace GreatIdeas.Blazor.MudComponents
{
    public class IdeasCardBase: IdeasComponentBase
    {
        ///<summary>Add Components to footer of card</summary>
        [Parameter]
        public RenderFragment Actions { get; set; }

        ///<summary>Enable avator in card header</summary>
        [Parameter]
        public bool ShowAvator { get; set; }

        ///<summary>Add buttons to card header</summary>
        [Parameter]
        public RenderFragment HeaderActions { get; set; }
        
        ///<summary>Image to display</summary>
        [Parameter]
        public string Image { get; set; }

        string Initials() => Title.GetInitials("");
        
        ///<summary>Class to apply css</summary>
        [Parameter]
        public Color Color { get; set; }

    }
}
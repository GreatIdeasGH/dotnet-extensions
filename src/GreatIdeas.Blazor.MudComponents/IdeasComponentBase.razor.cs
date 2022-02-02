using Microsoft.AspNetCore.Components;

namespace GreatIdeas.Blazor.MudComponents
{
    public class IdeasComponentBase: ComponentBase
    {
        ///<summary>Class to apply css</summary>
        [Parameter]
        public string Class { get; set; } = "mt-2";
        
        ///<summary>Title of card</summary> 
        [Parameter]
        public string Title { get; set; }

        ///<summary>Subtitle of card</summary>
        [Parameter]
        public string SubTitle { get; set; }

        ///<summary>Breakpoint for Small to medium tablet</summary>
        [Parameter]
        public int sm { get; set; } = 6;
        ///<summary>Breakpoint for Small to large phone</summary>
        [Parameter]
        public int xs { get; set; } = 12;
        ///<summary>Breakpoint for Large tablet to laptop</summary>
        [Parameter]
        public int md { get; set; } = 4;
        ///<summary>Breakpoint for Desktop</summary>
        [Parameter]
        public int lg { get; set; }

        ///<summary>Add Components to body of paper</summary>
        [Parameter]
        public RenderFragment Content { get; set; }

        ///<summary>Select Icon for display</summary>
        [Parameter]
        public string Icon { get; set; }

        ///<summary>Enable Icon</summary>
        [Parameter]
        public bool ShowIcon { get; set; } = true;
    }
}
using System;
using Microsoft.AspNetCore.Components;

namespace GreatIdeas.Blazor.MudComponents
{
    public class IdeasSearchBase: IdeasComponentBase
    {
        protected string SearchTerm { get; set; }
        
        [Parameter] public EventCallback<string> OnSearchChanged { get; set; }
        
        protected void SearchChanged()
        {
            // if (SearchTerm.Length > 1 && !string.IsNullOrWhiteSpace(SearchTerm))
            {
                OnSearchChanged.InvokeAsync(SearchTerm);
            }
        }

        protected virtual void ClearSearch()
        {
            SearchTerm = String.Empty;
            OnSearchChanged.InvokeAsync(SearchTerm);
            
        }
    }
}
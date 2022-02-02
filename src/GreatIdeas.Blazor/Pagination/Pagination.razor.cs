using System.Collections.Generic;
using System.Threading.Tasks;
using GreatIdeas.Extensions.Paging;
using Microsoft.AspNetCore.Components;

namespace GreatIdeas.Blazor.Pagination
{
    public partial class Pagination: ComponentBase
    {
        [Parameter]
        public Metadata Metadata { get; set; }
       /*[Parameter] public int CurrentPage { get; set; }
       [Parameter] public int TotalPages { get; set; }*/
       [Parameter] public int Radius { get; set; } = 2;
       [Parameter] public EventCallback<int> SelectedPage { get; set; }

       private List<LinkModel> Links;
       
       protected override void OnParametersSet()
       {
           LoadPages();
       }

       private async Task OnSelectedPage(LinkModel link)
       {
           if (link.Page == Metadata.PageIndex)
           {
               return;
           }

           if (!link.Enabled)
           {
               return;
           }

           Metadata.PageIndex = link.Page;
           await SelectedPage.InvokeAsync(link.Page);
       }

       private void LoadPages()
       {
           Links = new List<LinkModel>();
           var isPreviousPageLinkEnabled = Metadata.PageIndex != 1;
           var previousPage = Metadata.PageIndex - 1;
           Links.Add(new LinkModel(previousPage, isPreviousPageLinkEnabled, "Prev"));

           for (int i = 1; i <= Metadata.TotalPages; i++)
           {
               if (i >= Metadata.PageIndex - Radius && i <= Metadata.PageIndex + Radius)
               {
                   Links.Add(new LinkModel(i) { Active = Metadata.PageIndex == i });
               }
           }

           var isNextPageLinkEnabled = Metadata.PageIndex != Metadata.TotalPages;
           var nextPage = Metadata.PageIndex + 1;
           Links.Add(new LinkModel(nextPage, isNextPageLinkEnabled, "Next"));
       }
    }

    class LinkModel
    {
        public LinkModel(int page)
            : this(page, true) { }

        public LinkModel(int page, bool enabled)
            : this(page, enabled, page.ToString())
        { }

        public LinkModel(int page, bool enabled, string text)
        {
            Page = page;
            Enabled = enabled;
            Text = text;
        }

        /// <summary>
        /// Pagination label
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Current page
        /// </summary>
        public int Page { get; set; }
        
        /// <summary>
        /// Is current page enabled
        /// </summary>
        public bool Enabled { get; set; } = true;
        
        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set; } = false;
    }
}
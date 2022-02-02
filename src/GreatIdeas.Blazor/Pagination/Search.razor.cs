using System.Timers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Timer = System.Timers.Timer;

namespace GreatIdeas.Blazor.Pagination
{
    public partial class Search
    {
        private Timer _timer = new ();
        public string SearchTerm { get; set; }
        
        public InputText SearchBox { get; set; }
        
        [Parameter] public EventCallback<string> OnSearchChanged { get; set; }
        
        private void SearchChanged()
        {
            OnSearchChanged.InvokeAsync(SearchTerm);
            // if (SearchTerm.Length >= 2)
            // {
            //     OnSearchChanged.InvokeAsync(SearchTerm);
            // }

            // _timer?.Dispose();
            //
            // _timer = new Timer {Interval = 500, Enabled = true};
            // _timer.Elapsed += OnTimerElapsed;
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnSearchChanged.InvokeAsync(SearchTerm);
            _timer.Dispose();
        }
    }
}
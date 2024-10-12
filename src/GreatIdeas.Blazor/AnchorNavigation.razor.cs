using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace GreatIdeas.Blazor;

public partial class AnchorNavigation : IDisposable
{

    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] IJSRuntime JSRuntime { get; set; } = default!;

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await ScrollToFragment();
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
        GC.SuppressFinalize(this);
    }

    private async void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        await ScrollToFragment();
    }

    private async ValueTask ScrollToFragment()
    {
        var uri = new Uri(NavigationManager.Uri, UriKind.Absolute);
        var fragment = uri.Fragment;
        if (fragment.StartsWith('#'))
        {
            // Handle text fragment (https://example.org/#test:~:text=foo)
            // https://github.com/WICG/scroll-to-text-fragment/
            var elementId = fragment[1..];
            var index = elementId.IndexOf(":~:", StringComparison.Ordinal);
            if (index > 0)
            {
                elementId = elementId[..index];
            }

            if (!string.IsNullOrEmpty(elementId))
            {
                await JSRuntime.InvokeVoidAsync("ScrollToId", elementId);
            }
        }
    }
}
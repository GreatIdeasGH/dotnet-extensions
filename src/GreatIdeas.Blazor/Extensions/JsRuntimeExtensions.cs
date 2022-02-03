using Microsoft.JSInterop;

namespace GreatIdeas.Blazor.Extensions;

public static class JsRuntimeExtensions
{
    public static async ValueTask InitializeInactivityTimer<T>(
        this IJSRuntime js,
        DotNetObjectReference<T> dotNetObjectReference)
        where T : class
    {
        await js.InvokeVoidAsync("initializeInactivityTimer", dotNetObjectReference);
    }

    public static ValueTask SaveAs(
        this IJSRuntime jsRuntime,
        string fileName,
        byte[] content)
    {
        return jsRuntime.InvokeVoidAsync("saveAsFile", fileName, Convert.ToBase64String(content));
    }

    public static async ValueTask BrowserOnlineHandler<T>(this IJSRuntime jsRuntime, DotNetObjectReference<T> dotNetObjectReference) where T : class
    {
        await jsRuntime.InvokeVoidAsync("blazorInterop.registerOnlineHandler", dotNetObjectReference);
    }
}
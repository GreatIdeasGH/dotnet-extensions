using Microsoft.JSInterop;

namespace GreatIdeas.Blazor.Extensions;

public static class JsRuntimeExtensions
{
    /// <summary>
    /// Initialize the inactivity timer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="js"></param>
    /// <param name="dotNetObjectReference"></param>
    /// <returns></returns>
    public static async ValueTask InitializeInactivityTimer<T>(
        this IJSRuntime js,
        DotNetObjectReference<T> dotNetObjectReference)
        where T : class
    {
        await js.InvokeVoidAsync("initializeInactivityTimer", dotNetObjectReference);
    }

    /// <summary>
    /// Save a file to the client's local file system
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="fileName"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public static ValueTask SaveAs(
        this IJSRuntime jsRuntime,
        string fileName,
        byte[] content)
    {
        return jsRuntime.InvokeVoidAsync("saveAsFile", fileName, Convert.ToBase64String(content));
    }

    /// <summary>
    /// Online status indicator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsRuntime"></param>
    /// <param name="dotNetObjectReference"></param>
    /// <returns></returns>
    public static async ValueTask BrowserOnlineHandler<T>(this IJSRuntime jsRuntime, DotNetObjectReference<T> dotNetObjectReference) where T : class
    {
        await jsRuntime.InvokeVoidAsync("blazorInterop.registerOnlineHandler", dotNetObjectReference);
    }
}
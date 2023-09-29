namespace Cadenza.Interop;

public class NavigationInterop : INavigation
{
    private readonly IJSRuntime _jsRuntime;
    private readonly NavigationManager _navigationManager;

    public NavigationInterop(IJSRuntime jsRuntime, NavigationManager navigationManager)
    {
        _jsRuntime = jsRuntime;
        _navigationManager = navigationManager;
    }

    public Task NavigateTo(string url)
    {
        _navigationManager.NavigateTo(url);
        return Task.CompletedTask;
    }

    public async Task OpenNewTab(string url)
    {
        await _jsRuntime.InvokeVoidAsync("open", url, "_blank");
    }
}

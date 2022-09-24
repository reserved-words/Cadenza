using Cadenza.Web.Common.Interop;
using Microsoft.JSInterop;

namespace Cadenza.Interop
{
    public class NavigationInterop : INavigation
    {
        private readonly IJSRuntime _jsRuntime;

        public NavigationInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task OpenNewTab(string url)
        {
            await _jsRuntime.InvokeVoidAsync("open", url, "_blank");
        }
    }
}

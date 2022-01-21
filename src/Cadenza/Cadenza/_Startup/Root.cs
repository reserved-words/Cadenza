using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Cadenza
{
    public static class ComponentRegistration
    {
        public static WebAssemblyHostBuilder RegisterComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            return builder;
        }
    }
}

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Cadenza
{
    public static class ComponentRegistration
    {
        public static WebAssemblyHostBuilder RegisterComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");
            return builder;
        }
    }
}

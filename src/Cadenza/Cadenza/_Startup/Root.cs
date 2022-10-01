namespace Cadenza._Startup
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

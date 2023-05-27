using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Cadenza.HttpMessageHandlers;

public class LocalApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public LocalApiAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigation, IConfiguration config)
        : base(provider, navigation)
    {
        var baseUrl = config["LocalApi:BaseUrl"];
        var scope = config["Authentication:Scopes:Local"];

        ConfigureHandler(
            authorizedUrls: new[] { baseUrl },
            scopes: new List<string> { scope });
    }
}
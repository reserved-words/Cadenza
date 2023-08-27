using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Cadenza.HttpMessageHandlers;

public class MainApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public MainApiAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigation, IConfiguration config)
        : base(provider, navigation)
    {
        var baseUrl = config["DatabaseApi:BaseUrl"];
        var scope = config["AppAuthentication:Scopes:Database"];

        ConfigureHandler(
            authorizedUrls: new[] { baseUrl },
            scopes: new List<string> { scope });
    }
}

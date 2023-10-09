namespace Cadenza.Web.Actions.Effects;

public class NavigationEffects
{
    private readonly INavigation _navigation;

    public NavigationEffects(INavigation navigation)
    {
        _navigation = navigation;
    }

    [EffectMethod]
    public async Task HandleApplicationStartRequest(NavigationRequest action, IDispatcher dispatcher)
    {
        if (action.NewTab)
        {
            await _navigation.OpenNewTab(action.Url);
        }
        else
        {
            await _navigation.NavigateTo(action.Url);
        }
    }
}

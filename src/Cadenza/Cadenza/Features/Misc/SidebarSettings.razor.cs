namespace Cadenza.Features.Misc;

public partial class SidebarSettings
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<ViewState> ViewState { get; set; }

    private void OnViewSettings()
    {
        Dispatcher.Dispatch(new ViewTabRequest(Tab.Settings));
    }
}

namespace Cadenza.Layout;

public class TabsLayoutBase : ComponentBase
{
    [Inject] public IState<ViewState> ViewState { get; set; }

    protected MudTabs tabs;

    protected override void OnInitialized()
    {
        ViewState.StateChanged += ViewState_StateChanged;
        base.OnInitialized();
    }

    private void ViewState_StateChanged(object sender, EventArgs args)
    {
        var switchToTab = ViewState.Value.CurrentTab == Tab.Default
            ? Tab.Home
            : ViewState.Value.CurrentTab;

        tabs.ActivatePanel(switchToTab);
    }
}

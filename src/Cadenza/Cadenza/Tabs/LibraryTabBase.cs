namespace Cadenza;

public class LibraryTabBase : ComponentBase
{
    [Inject]
    public IMessenger Messenger { get; set; }

    protected ViewItem? CurrentItem = null;

    protected override void OnInitialized()
    {
        Messenger.Subscribe<ViewItemEventArgs>(OnViewItem);
    }

    private Task OnViewItem(object sender, ViewItemEventArgs e)
    {
        CurrentItem = e.Item;
        StateHasChanged();
        return Task.CompletedTask;
    }
}

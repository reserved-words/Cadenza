namespace Cadenza.Features.Player.Components;

public partial class PlayerControls : ComponentBase, IDisposable
{
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IJSRuntime JSRuntime { get; set; }

    [Parameter] public bool IsTrackPopulated { get; set; }
    [Parameter] public bool IsLastTrack { get; set; }

    protected bool CanPause { get; set; }
    protected bool CanPlay { get; set; }

    protected bool CanSkipNext => IsTrackPopulated && !IsLastTrack;
    protected bool CanSkipPrevious => IsTrackPopulated;

    protected override void OnParametersSet()
    {
        CanPlay = false;
        CanPause = IsTrackPopulated;
    }

    private DotNetObjectReference<PlayerControls> _dotNetHelper;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetHelper = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("BrowserPlayerControls.setDotNetHelper", _dotNetHelper);
        }
    }

    [JSInvokable]
    public void Pause()
    {
        CanPlay = true;
        CanPause = false;
        StateHasChanged();
        Dispatcher.Dispatch(new PlayerControlsPauseRequest());
    }

    [JSInvokable]
    public void Resume()
    {
        CanPlay = false;
        CanPause = true;
        StateHasChanged();
        Dispatcher.Dispatch(new PlayerControlsResumeRequest());
    }

    [JSInvokable]
    public void SkipNext()
    {
        Dispatcher.Dispatch(new PlayerControlsNextRequest());
    }

    [JSInvokable]
    public void SkipPrevious()
    {
        Dispatcher.Dispatch(new PlayerControlsPreviousRequest());
    }

    public void Dispose()
    {
        if (_dotNetHelper is not null)
        {
            _dotNetHelper.Dispose();
        }
    }
}

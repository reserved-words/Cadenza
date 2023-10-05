namespace Cadenza.Web.Components.Player;

public class CurrentTrackProgressBase : FluxorComponent
{
    [Inject] private IState<PlayProgressState> PlayProgressState { get; set; }

    public double Progress => PlayProgressState.Value.Progress;
    public int SecondsPlayed => PlayProgressState.Value.SecondsPlayed;
    public int SecondsRemaining => PlayProgressState.Value.SecondsRemaining;
}

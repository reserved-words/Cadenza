namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlayProgressState(double Progress, int SecondsPlayed, int SecondsRemaining, int TotalSeconds)
{
    private static PlayProgressState Init() => new PlayProgressState(0, 0, 0, 0);
}

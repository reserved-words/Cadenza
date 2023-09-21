namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ApplicationState(bool StartedUp, bool Success)
{
    private static ApplicationState Init() => new ApplicationState(false, true);
}

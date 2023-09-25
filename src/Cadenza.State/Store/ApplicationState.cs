namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ApplicationState(bool Started)
{
    private static ApplicationState Init() => new ApplicationState(false);
}

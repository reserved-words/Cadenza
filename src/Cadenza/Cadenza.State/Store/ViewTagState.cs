namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewTagState(bool IsLoading, string Tag, IReadOnlyCollection<PlayerItemVM> Items) 
{
    private static ViewTagState Init() => new ViewTagState(true, null, null);
}
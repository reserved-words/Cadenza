namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewTagState(bool IsLoading, string Tag, IReadOnlyCollection<TaggedItemVM> Items)
{
    private static ViewTagState Init() => new ViewTagState(true, null, null);
}
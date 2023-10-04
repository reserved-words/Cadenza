using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record GroupingsState(bool IsLoading, List<Grouping> Groupings)
{
    private static GroupingsState Init() => new GroupingsState(false, new List<Grouping>());
}

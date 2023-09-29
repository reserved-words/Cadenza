using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewGroupingState(bool IsLoading, Grouping Grouping, List<string> Genres) 
{
    private static ViewGroupingState Init() => new ViewGroupingState(true, null, null);
}
﻿namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewGroupingState(bool IsLoading, string Grouping, IReadOnlyCollection<string> Genres)
{
    private static ViewGroupingState Init() => new ViewGroupingState(true, null, null);
}
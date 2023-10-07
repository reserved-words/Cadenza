namespace Cadenza.State.Actions;

public record FetchViewGroupingRequest(GroupingVM Grouping);

public record FetchViewGroupingResult(GroupingVM Grouping, IReadOnlyCollection<string> Genres);

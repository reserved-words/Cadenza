namespace Cadenza.Web.State.Actions;

public record FetchViewGroupingRequest(string Grouping);

public record FetchViewGroupingResult(string Grouping, IReadOnlyCollection<string> Genres);

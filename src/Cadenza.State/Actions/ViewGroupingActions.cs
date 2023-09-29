using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record FetchViewGroupingRequest(Grouping Grouping);

public record FetchViewGroupingResult(Grouping Grouping, List<string> Genres);

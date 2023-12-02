using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record FetchViewGroupingRequest(GroupingVM Grouping);

public record FetchViewGroupingResult(GroupingVM Grouping, IReadOnlyCollection<string> Genres);

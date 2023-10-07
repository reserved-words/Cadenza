namespace Cadenza.State.Actions;

public record FetchGroupingsRequest();
public record FetchGroupingsResult(IReadOnlyCollection<GroupingVM> Result);
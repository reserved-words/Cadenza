namespace Cadenza.Web.State.Actions;

public record FetchGroupingsRequest();
public record FetchGroupingsResult(IReadOnlyCollection<string> Result);
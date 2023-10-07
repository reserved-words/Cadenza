namespace Cadenza.State.Actions;

public record FetchViewTagRequest(string Tag);

public record FetchViewTagResult(string Tag, IReadOnlyCollection<PlayerItemVM> Items);

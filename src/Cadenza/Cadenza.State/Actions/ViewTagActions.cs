namespace Cadenza.State.Actions;

public record FetchViewTagRequest(string Tag);

public record FetchViewTagResult(string Tag, List<PlayerItemVM> Items);

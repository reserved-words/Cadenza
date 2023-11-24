namespace Cadenza.Web.State.Actions;

public record FetchViewTagRequest(string Tag);

public record FetchViewTagResult(string Tag, IReadOnlyCollection<TaggedItemVM> Items);

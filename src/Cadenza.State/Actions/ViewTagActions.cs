using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Actions;

public record FetchViewTagRequest(string Tag);

public record FetchViewTagResult(string Tag, List<PlayerItem> Items);

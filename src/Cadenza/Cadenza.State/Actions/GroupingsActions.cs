using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record FetchGroupingsRequest();
public record FetchGroupingsResult(List<Grouping> Result);
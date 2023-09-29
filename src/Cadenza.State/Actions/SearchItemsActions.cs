using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Actions;

public record SearchItemsUpdateRequest();

public record SearchItemsUpdatedAction(List<PlayerItem> Result);
